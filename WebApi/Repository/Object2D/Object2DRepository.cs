using Dapper;
using WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WebApi.Repositories
{
    public class Object2DRepository : IObject2DRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<Object2DRepository> _logger;

        public Object2DRepository(IDbConnection dbConnection, ILogger<Object2DRepository> logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }

        public async Task<IEnumerable<Object2d>> GetAllObject2DsAsync()
        {
            try
            {
                _logger.LogInformation("🔍 Fetching all Object2D records...");

                string sql = "SELECT * FROM Object2D";
                if (_dbConnection.State != ConnectionState.Open) _dbConnection.Open();
                var result = await _dbConnection.QueryAsync<Object2d>(sql);

                _logger.LogInformation($"✅ Retrieved {result.AsList().Count} Object2D records.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR in GetAllObject2DsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Object2d?> GetObject2DByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"🔍 Fetching object with ID: {id}");

                string sql = "SELECT * FROM Object2D WHERE Id = @Id";
                if (_dbConnection.State != ConnectionState.Open) _dbConnection.Open();
                var obj = await _dbConnection.QueryFirstOrDefaultAsync<Object2d>(sql, new { Id = id });

                if (obj != null)
                    _logger.LogInformation("✅ Object retrieved successfully.");
                else
                    _logger.LogWarning($"⚠️ No object found with ID: {id}");

                return obj;
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR in GetObject2DByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task AddObject2DAsync(Object2d object2D)
        {
            try
            {
                _logger.LogInformation($"📝 Inserting new object with ID: {object2D.Id}");

                string sql = @"INSERT INTO Object2D (Id, PrefabId, X_Position, Y_Position, ScaleX, ScaleY, RotationZ, SortingLayer, EnvironmentId) 
                               VALUES (@Id, @PrefabId, @X_Position, @Y_Position, @ScaleX, @ScaleY, @RotationZ, @SortingLayer, @EnvironmentId)";

                if (_dbConnection.State != ConnectionState.Open) _dbConnection.Open();

                using (var transaction = _dbConnection.BeginTransaction())
                {
                    int rowsAffected = await _dbConnection.ExecuteAsync(sql, new
                    {
                        Id = object2D.Id,
                        PrefabId = object2D.PrefabId,
                        X_Position = object2D.X_Position,
                        Y_Position = object2D.Y_Position,
                        ScaleX = object2D.ScaleX,
                        ScaleY = object2D.ScaleY,
                        RotationZ = object2D.RotationZ,
                        SortingLayer = object2D.SortingLayer,
                        EnvironmentId = object2D.EnvironmentId
                    }, transaction);

                    if (rowsAffected > 0)
                    {
                        _logger.LogInformation("✅ Object inserted successfully.");
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
                _logger.LogError($"❌ ERROR in AddObject2DAsync: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateObject2DAsync(Object2d object2D)
        {
            try
            {
                _logger.LogInformation($"🔄 Updating object with ID: {object2D.Id}");

                string sql = @"UPDATE Object2D 
                               SET PrefabId = @PrefabId, X_Position = @X_Position, Y_Position = @Y_Position, ScaleX = @ScaleX, ScaleY = @ScaleY, RotationZ = @RotationZ, SortingLayer = @SortingLayer, EnvironmentId = @EnvironmentId 
                               WHERE Id = @Id";

                if (_dbConnection.State != ConnectionState.Open) _dbConnection.Open();

                using (var transaction = _dbConnection.BeginTransaction())
                {
                    int rowsAffected = await _dbConnection.ExecuteAsync(sql, new
                    {
                        Id = object2D.Id,
                        PrefabId = object2D.PrefabId,
                        X_Position = object2D.X_Position,
                        Y_Position = object2D.Y_Position,
                        ScaleX = object2D.ScaleX,
                        ScaleY = object2D.ScaleY,
                        RotationZ = object2D.RotationZ,
                        SortingLayer = object2D.SortingLayer,
                        EnvironmentId = object2D.EnvironmentId
                    }, transaction);

                    if (rowsAffected > 0)
                    {
                        _logger.LogInformation("✅ Object updated successfully.");
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
                _logger.LogError($"❌ ERROR in UpdateObject2DAsync: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteObject2DAsync(string id)
        {
            try
            {
                _logger.LogInformation($"🗑 Deleting object with ID: {id}");

                string sql = "DELETE FROM Object2D WHERE Id = @Id";

                if (_dbConnection.State != ConnectionState.Open) _dbConnection.Open();

                using (var transaction = _dbConnection.BeginTransaction())
                {
                    int rowsAffected = await _dbConnection.ExecuteAsync(sql, new { Id = id }, transaction);

                    if (rowsAffected > 0)
                    {
                        _logger.LogInformation("✅ Object deleted successfully.");
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
                _logger.LogError($"❌ ERROR in DeleteObject2DAsync: {ex.Message}");
                throw;
            }
        }
    }
}
