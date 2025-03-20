using Dapper;
using WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WebApi.Repositories
{
    public class Environment2DRepository : IEnvironment2DRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<Environment2DRepository> _logger;

        public Environment2DRepository(IDbConnection dbConnection, ILogger<Environment2DRepository> logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }

        public async Task<IEnumerable<Environment2d>> GetAllEnvironment2DsAsync(string userId)
        {
            try
            {
                _logger.LogInformation("🔍 Fetching all Environment2 records for user...");

                string sql = "SELECT Id, Name, MaxLength, MaxHeight, UserId FROM Environment2 WHERE UserId = @UserId";
                if (_dbConnection.State != ConnectionState.Open) _dbConnection.Open();
                var result = await _dbConnection.QueryAsync<Environment2d>(sql, new { UserId = userId });

                _logger.LogInformation($"✅ Retrieved {result.AsList().Count} Environment2 records for user.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR in GetAllEnvironment2DsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Environment2d?> GetWorldByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"🔍 Fetching world with ID: {id}");

                string sql = "SELECT Id, Name, MaxLength, MaxHeight, UserId FROM Environment2 WHERE Id = @Id";
                if (_dbConnection.State != ConnectionState.Open) _dbConnection.Open();
                var world = await _dbConnection.QueryFirstOrDefaultAsync<Environment2d>(sql, new { Id = id });

                if (world != null)
                    _logger.LogInformation("✅ World retrieved successfully.");
                else
                    _logger.LogWarning($"⚠️ No world found with ID: {id}");

                return world;
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR in GetWorldByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task AddWorldAsync(Environment2d environment2D)
        {
            try
            {
                _logger.LogInformation($"📝 Inserting new world with ID: {environment2D.Id}");

                string sql = "INSERT INTO Environment2 (Id, Name, MaxLength, MaxHeight, UserId) VALUES (@Id, @Name, @MaxLength, @MaxHeight, @UserId)";

                if (_dbConnection.State != ConnectionState.Open) _dbConnection.Open();

                using (var transaction = _dbConnection.BeginTransaction())
                {
                    int rowsAffected = await _dbConnection.ExecuteAsync(sql, new
                    {
                        Id = environment2D.Id,
                        Name = environment2D.Name,
                        MaxLength = environment2D.MaxLength,
                        MaxHeight = environment2D.MaxHeight,
                        UserId = environment2D.UserId
                    }, transaction);

                    if (rowsAffected > 0)
                    {
                        _logger.LogInformation("✅ World inserted successfully.");
                        transaction.Commit();
                    }
                    else
                    {
                        _logger.LogWarning("⚠️ INSERT executed, but no rows affected.");
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR in AddWorldAsync: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateWorldAsync(Environment2d environment2D)
        {
            try
            {
                _logger.LogInformation($"🔄 Updating world with ID: {environment2D.Id}");

                string sql = @"UPDATE Environment2 
                               SET Name = @Name, MaxLength = @MaxLength, MaxHeight = @MaxHeight, UserId = @UserId
                               WHERE Id = @Id";

                if (_dbConnection.State != ConnectionState.Open) _dbConnection.Open();

                using (var transaction = _dbConnection.BeginTransaction())
                {
                    int rowsAffected = await _dbConnection.ExecuteAsync(sql, new
                    {
                        Id = environment2D.Id,
                        Name = environment2D.Name,
                        MaxLength = environment2D.MaxLength,
                        MaxHeight = environment2D.MaxHeight,
                        UserId = environment2D.UserId
                    }, transaction);

                    if (rowsAffected > 0)
                    {
                        _logger.LogInformation("✅ World updated successfully.");
                        transaction.Commit();
                    }
                    else
                    {
                        _logger.LogWarning("⚠️ UPDATE executed, but no rows affected.");
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR in UpdateWorldAsync: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteWorldAsync(int id)
        {
            try
            {
                _logger.LogInformation($"🗑 Deleting world with ID: {id}");

                string sql = "DELETE FROM Environment2 WHERE Id = @Id";

                if (_dbConnection.State != ConnectionState.Open) _dbConnection.Open();

                using (var transaction = _dbConnection.BeginTransaction())
                {
                    int rowsAffected = await _dbConnection.ExecuteAsync(sql, new { Id = id }, transaction);

                    if (rowsAffected > 0)
                    {
                        _logger.LogInformation("✅ World deleted successfully.");
                        transaction.Commit();
                    }
                    else
                    {
                        _logger.LogWarning($"⚠️ DELETE executed, but no rows affected.");
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR in DeleteWorldAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Object2d>> GetObjectsForWorld(int worldId)
        {
            try
            {
                _logger.LogInformation($"🔍 Fetching objects for World ID: {worldId}");

                string sql = "SELECT * FROM Object2D WHERE EnvironmentId = @WorldId";

                if (_dbConnection.State != ConnectionState.Open) _dbConnection.Open();
                var objects = await _dbConnection.QueryAsync<Object2d>(sql, new { WorldId = worldId });

                _logger.LogInformation($"✅ Retrieved {objects.AsList().Count} objects.");
                return objects;
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR in GetObjectsForWorld: {ex.Message}");
                throw;
            }
        }
    }
}
