using Confluent.Kafka;
using ProducerService.HostedServices;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ProducerConfig>(new ProducerConfig() { BootstrapServers = "localhost:29092" });
builder.Services.AddSingleton<ConsumerConfig>(new ConsumerConfig() { BootstrapServers = "localhost:29092", GroupId = "1" });
builder.Services.AddHostedService<ConsumerService>();

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
