using LudumDare29.DataTypes;
using FlatRedBall.TileGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using LudumDare29.Performance;
using FlatRedBall.Graphics;
using System.Reflection;

namespace FlatRedBall.TileEntities
{
    public static class TileEntityInstantiator
    {
        public static void CreateEntitiesFrom(LayeredTileMap layeredTileMap, Dictionary<string, TileMapInfo> tileMapInfos)
        {
            // prob need to clear out the tileShapeCollection
            var entitiesToRemove = new List<string>();
            float dimension = float.NaN;
            float dimensionHalf = 0;
            foreach (var layer in layeredTileMap.MapLayers)
            {

                var dictionary = layer.NamedTileOrderedIndexes;
                foreach (var info in tileMapInfos)
                {
                    if (!string.IsNullOrEmpty(info.Value.EntityToCreate) && dictionary.ContainsKey(info.Key))
                    {

                        var factories = Assembly.GetExecutingAssembly()
                            .GetTypes()
                            .Where(t => t.GetInterfaces().Contains(typeof (IEntityFactory))
                                        && t.GetConstructor(Type.EmptyTypes) != null)
                            .Select(
                                t =>
                                {
                                    var propertyInfo = t.GetProperty("Self");
                                    var value = propertyInfo.GetValue(null, null);
                                    return value as IEntityFactory;
                                }).ToList();

                        foreach (var factory in factories)
                        {
                            var type = factory.GetType();
                            var methodInfo = type.GetMethod("CreateNew", new[] {typeof (Layer)});
                            var returntypeString = methodInfo.ReturnType.Name;

                            if (info.Value.EntityToCreate.EndsWith(returntypeString))
                            {
                                entitiesToRemove.Add(info.Value.EntityToCreate);
                                var indexList = dictionary[info.Key];

                                foreach (var index in indexList)
                                {
                                    float left;
                                    float bottom;
                                    layer.GetBottomLeftWorldCoordinateForOrderedTile(index, out left, out bottom);

                                    if (float.IsNaN(dimension))
                                    {
                                        dimension = layer.Vertices[index + 1].Position.X - left;
                                        dimensionHalf = dimension/2.0f;
                                    }

                                    var entity = factory.CreateNew() as PositionedObject;


                                    if (entity != null)
                                    {
                                        entity.X = left + dimensionHalf;
                                        entity.Y = bottom + dimensionHalf;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach (var entityToRemove in entitiesToRemove)
            {
                string remove = entityToRemove;
                layeredTileMap.RemoveTiles(t => t.EntityToCreate == remove, tileMapInfos.Values);
            }
        }
    }
}
