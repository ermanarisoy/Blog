using AutoMapper;
using Cache.API.Entities;
using Cache.API.Interface;
using EventBus.Messages.Events;
using MassTransit;
using MassTransit.Mediator;

namespace Cache.API.EventBusConsumer
{
    public class SubjectConsumer : IConsumer<SubjectEvent>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SubjectConsumer> _logger;
        private readonly ISubjectRepository _subjectRepository;

        public SubjectConsumer(IMapper mapper, ILogger<SubjectConsumer> logger, ISubjectRepository subjectRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subjectRepository = subjectRepository ?? throw new ArgumentNullException(nameof(subjectRepository));
        }

        public async Task Consume(ConsumeContext<SubjectEvent> context)
        {
            var command = _mapper.Map<Subject>(context.Message);
            _subjectRepository.CreateSubject(command);
            _logger.LogInformation("BasketCheckoutEvent consumed successfully.");
        }
    }
}
