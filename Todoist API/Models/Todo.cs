﻿namespace Todoist_API.Models;

public class Todo
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTime? Date { get; set; }
    public string? Description { get; set; }
    public string? Project { get; set; }
    public Tag[]? Tags { get; set; }
    public string? UserId { get; set; }
}
