
using InventoryService.Constants.Event;
using InventoryService.Domain.Logging.Listeners;
using InventoryService.Infrastructure.Integrations.NATs;

namespace InventoryService.Infrastructure.BackgroundHosted
{
    public class NATsTask(
        IServiceScopeFactory serviceScopeFactory,
        NATsIntegration _natsIntegration
        )
    {
        public void Listen()
        {
            /** Init all task listeners here */

            /*==================== Other Module ====================*/
        }

        public void ConsumeJetStream() 
        {
            /*==================== Logging ====================*/
            _ = _natsIntegration.InitPullListenerTask<LoggingNATsListener>(serviceScopeFactory,
                NATsEventStreamModule.JETSTREAM_PAMA.ToString(),
                LoggingCallEventConstant.SUBS_LOGGER_SUBJECT
            );
        }

        public void ListenAndReply()
        {
            /** Init all task listeners here */

            /*==================== Logging ====================*/
            _natsIntegration.InitListenAndReplyTask<LoggingNATsListenAndReply>(serviceScopeFactory,
                _natsIntegration.Subject(
                    NATsEventModuleEnum.LOGGER,
                    NATsEventActionEnum.DEBUG,
                    NATsEventStatusEnum.INFO
                )
            );

            /*==================== Product ====================*/
            

            /*==================== Other Module ====================*/
        }
    }
}
