﻿namespace FormulaOne.Entities.Dtos.Requests;

public class CreateDriverAchievementRequest
{
    public Guid Driverid { get; set; }
    public int WorldChampionship { get; set; }
    public int PolePosition { get; set; }
    public int Wins { get; set; }
}