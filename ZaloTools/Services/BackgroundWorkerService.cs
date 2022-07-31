namespace ZaloTools.Services;

public class BackgroundWorkerService : IBackgroundWorkerService
{
    private readonly BackgroundWorker _worker = new BackgroundWorker();
    private readonly DispatcherTimer _timer = new DispatcherTimer();
    public BackgroundWorkerService()
    {
        _worker.WorkerReportsProgress = true;
        _worker.WorkerSupportsCancellation = true;
        _worker.DoWork += WorkerOnDoWork;
        _worker.ProgressChanged += WorkerOnProgressChanged;
        _worker.RunWorkerCompleted += WorkerOnRunWorkerCompleted;
    }

    private void WorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {

    }

    private void WorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
    {

    }

    private void WorkerOnDoWork(object sender, DoWorkEventArgs e)
    {

    }

    public Task StopAsync()
    {
        _timer?.Start();
        _worker?.RunWorkerAsync();
        return Task.CompletedTask;
    }

    public Task StartAsync()
    {
        _timer?.Stop();
        _worker.CancelAsync();
        return Task.CompletedTask;
    }
}