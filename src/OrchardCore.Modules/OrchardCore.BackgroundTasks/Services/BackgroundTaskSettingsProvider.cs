/*
	Provides settings from 'background task',
	using 'background task manager class'
*/

using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace OrchardCore.BackgroundTasks.Services
{
    public class BackgroundTaskSettingsProvider : IBackgroundTaskSettingsProvider
    {
        private readonly BackgroundTaskManager _backgroundTaskManager;

        public BackgroundTaskSettingsProvider(BackgroundTaskManager backgroundTaskManager)
        {
            _backgroundTaskManager = backgroundTaskManager;
        }

        public IChangeToken ChangeToken => _backgroundTaskManager.ChangeToken;

        /*
			@return settings received from 'backgound task'
			        OR null
        */
        public async Task<BackgroundTaskSettings> GetSettingsAsync(IBackgroundTask task)
        {
            var document = await _backgroundTaskManager.GetDocumentAsync();

            if (document.Settings.TryGetValue(task.GetTaskName(), out var settings))
            {
                return settings;
            }

            return null;
        }
    }
}
