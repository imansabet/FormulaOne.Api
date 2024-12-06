﻿using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormulaOne.DataService.Repositories;

public class AchievementsRepository : GenericRepository<Achievement>, IAchievementsRepository
{
    public AchievementsRepository( AppDbContext context ,ILogger logger) : base(logger, context)
    {
    }

    public async Task<Achievement?> GetDriverAchievementAsync(Guid driverId)
    {
        try
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.DriverId == driverId);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} GetDriverAchievementAsync function error", typeof(AchievementsRepository));
            throw;
        }
    }
    public override async Task<IEnumerable<Achievement>> All()
    {
        try
        {
            return await _dbSet.Where(x => x.Status == 1)
                .AsNoTracking()
                .AsSplitQuery()
                .OrderBy(x => x.AddedDate)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} All function error", typeof(AchievementsRepository));
            throw;
        }
    }
    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            // get entity
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return false;

            result.Status = 0;
            result.UpdatedDate = DateTime.UtcNow;

            return true;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Delete function error", typeof(AchievementsRepository));
            throw;
        }
    }
    public override async Task<bool> Update(Achievement  achievement)
    {
        try
        {
            // get entity
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == achievement.Id);
            if (result == null)
                return false;

            result.UpdatedDate = DateTime.UtcNow;
            result.PolePosition = achievement.PolePosition;
            result.RaceWins = achievement.RaceWins;
            result.WorldChampionship = achievement.WorldChampionship;

            return true;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Update function error", typeof(AchievementsRepository));
            throw;
        }
    }
}