using Cache.API.EventBusConsumer;
using Cache.API.Interface;
using Cache.API.Repository;
using EventBus.Messages.Common;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
// Add services to the container.
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["CacheSettings:ConnectionString"];
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<SubjectConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
        {
            c.ConfigureConsumer<SubjectConsumer>(ctx);
        });
    });
});
builder.Services.AddMassTransitHostedService();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<SubjectConsumer>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
