using System;

public interface IIdentity
{
    Guid Id { get; set; }
    string Name { get; set; }
}