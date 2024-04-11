namespace Core.Interfaces.Services;

public interface IJwtProvider
{
    string Generate(IEnumerable<string> roles);
}