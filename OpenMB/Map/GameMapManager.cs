﻿using OpenMB.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenMB.Map
{
    public class GameMapManager
    {
        private static GameMapManager instance;
        public static GameMapManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameMapManager();
                }
                return instance;
            }
        }

        public GameMapManager()
        {
            maps = new Queue<IMap>();
        }

        private GameWorld world;
        private IMap currentMap;
        private Queue<IMap> maps;
        public void Load(string name)
        {
            if (maps.Count > 0)
            {
                maps.Dequeue().Destroy();
            }
            IMap map = new GameMap(name, world);
            maps.Enqueue(map);
            map.LoadMapStarted += Map_LoadMapStarted;
            map.LoadMapFinished += Map_LoadMapFinished;
            currentMap = map;
            map.LoadAsync();
        }

        private void Map_LoadMapFinished()
        {
        }

        private void Map_LoadMapStarted()
        {

        }

        public void Initization(GameWorld world)
        {
            this.world = world;
        }

        public void Dispose()
        {
            if (currentMap != null)
            {
                currentMap.Destroy();
            }
            maps.Clear();
        }

        public string GetCurrentMapName()
        {
            if (currentMap != null)
            {
                return currentMap.GetName();
            }
            else
            {
                return string.Empty;
            }
        }

        public GameMap GetCurrentMap()
        {
            return (GameMap)currentMap;
        }

        public void Update(float timeSinceLastFrame)
        {
            if (currentMap == null)
                return;
            currentMap.Update(timeSinceLastFrame);
        }
    }
}