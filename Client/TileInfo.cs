using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class TileInfo
    {
        public enum TileType { Floor, Wall, Door, OpenDoor };
        public TileType tileType = TileType.Floor;
        public Agent unitOnTile;
    }
}
