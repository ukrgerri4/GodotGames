using Godot;
using System;

public interface IPlayer
{
    int Id { get; set; }
    string NickName { get; set; }
    Device Device { get; set; }
}
