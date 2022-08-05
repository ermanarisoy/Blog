using Blog.API.Interface;
using Blog.API.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<BlogAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogAPIContext") ?? throw new InvalidOperationException("Connection string 'BlogAPIContext' not found.")));

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ConfigureEndpoints(ctx);
    });
});

// MassTransit-RabbitMQ Configuration
//builder.Services.AddMassTransit(config =>
//{

//    config.AddConsumer<BasketCheckoutConsumer>();

//    config.UsingRabbitMq((ctx, cfg) =>
//    {
//        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

//        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
//        {
//            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
//        });
//    });
//});
builder.Services.AddMassTransitHostedService();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<BlogAPIContext>();
    dataContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
