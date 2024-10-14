﻿namespace Backend.AnilibriaWorker.Models; 

public class FranchiseDto
{
    public string? Id { get; init; }
    
    public string? Name { get; init; }
    
    public List<ReleaseDto>? Releases { get; init; }
}