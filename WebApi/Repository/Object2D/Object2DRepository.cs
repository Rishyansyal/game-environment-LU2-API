//using Dapper;
//using WebApi.Models;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Threading.Tasks;

//namespace WebApi.Repositories
//{
//    public class Object2DRepository : IObject2DRepository
//    {
//        private readonly IDbConnection _dbConnection;

//        public Object2DRepository(IDbConnection dbConnection)
//        {
//            _dbConnection = dbConnection;
//        }

//        public async Task<IEnumerable<Object2D>> GetAllObject2DsAsync()
//        {
//            var sql = "SELECT * FROM Object2D";
//            return await _dbConnection.QueryAsync<Object2D>(sql);
//        }

//        public async Task<Object2D> GetObject2DByIdAsync(Guid id)
//        {
//            var sql = "SELECT * FROM Object2D WHERE Id = @Id";
//            return await _dbConnection.QueryFirstOrDefaultAsync<Object2D>(sql, new { Id = id });
//        }

//        public async Task AddObject2DAsync(Object2D object2D)
//        {
//            var sql = @"INSERT INTO Object2D (Id, PrefabId, PositionX, PositionY, ScaleX, ScaleY, RotationZ, SortingLayer, Environment2DID) 
//                        VALUES (@Id, @PrefabId, @PositionX, @PositionY, @ScaleX, @ScaleY, @RotationZ, @SortingLayer, @Environment2DID)";
//            await _dbConnection.ExecuteAsync(sql, object2D);
//        }

//        public async Task UpdateObject2DAsync(Object2D object2D)
//        {
//            var sql = @"UPDATE Object2D 
//                        SET PrefabId = @PrefabId, PositionX = @PositionX, PositionY = @PositionY, ScaleX = @ScaleX, ScaleY = @ScaleY, RotationZ = @RotationZ, SortingLayer = @SortingLayer, Environment2DID = @Environment2DID 
//                        WHERE Id = @Id";
//            await _dbConnection.ExecuteAsync(sql, object2D);
//        }

//        public async Task DeleteObject2DAsync(Guid id)
//        {
//            var sql = "DELETE FROM Object2D WHERE Id = @Id";
//            await _dbConnection.ExecuteAsync(sql, new { Id = id });
//        }
//    }
//}