var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddMutationConventions(true)
    .AddTestGraphQLNullabilityBugTypes();


var app = builder.Build();

app.MapGraphQL();

app.Run();


public class SomeClass
{
    public string? Message { get; set; }
}

public record TestError(string? Message = default);

public class Query
{
    public SomeClass GetClass() => new SomeClass();
}

public class Mutation
{
    public MutationResult<SomeClass, TestError> TestFakeMutation(bool error = false)
    {
        return error ? new TestError() : new SomeClass();
    }

    public MutationResult<TestError> DirectFail()
        => new TestError();
}

public class MutationType : ObjectType<Mutation>{ }
public class QueryType : ObjectType<Query> { }
