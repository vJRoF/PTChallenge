using System.Collections.Concurrent;
using System.ComponentModel;

namespace PTChallenge.Common;

/// <summary>
///     Предостаяляет экземпляры <see cref="Worker"/> с привязкой к идентификатору цепочки вычисления
/// </summary>
public class WorkerPool
{
    private readonly Worker.Factory _workerFactory;
    private ConcurrentDictionary<string, Worker> _workers = new();

    public WorkerPool(Worker.Factory workerFactory)
    {
        _workerFactory = workerFactory;
    }

    /// <summary>
    ///     Создаёт экземпляр <see cref="Worker"/> в пуле
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public Worker Create()
    {
        var worker = _workerFactory.Create();
        Add(worker);
        return worker;
    }

    /// <summary>
    ///     Получает экземпляр <see cref="Worker"/> из пула по <see cref="chainId"/> или создаёт новый
    /// </summary>
    /// <param name="chainId"></param>
    /// <returns></returns>
    public Worker GetOrCreate(string chainId)
    {
        if (_workers.TryGetValue(chainId, out var worker))
            return worker;
        
        worker = _workerFactory.Create(chainId);
        Add(worker);

        return worker;
    }

    private void Add(Worker worker)
    {
        if (!_workers.TryAdd(worker.ChainId, worker))
            throw new InvalidOperationException($"Worker с id {worker.ChainId} уже добавлен");
    }
}