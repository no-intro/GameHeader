namespace GameHeader.Systems
{
    using GameHeader.Abstract;
    using System;

    public class GeneralGame : Game
    {
        public GeneralGame(GameFileInfo game_info) : base(SystemType.General, game_info)
        {
        }

        public override string Dump()
        {
            return (base.Dump() + Util.Divider('='));
        }

        public override string System()
        {
            return SystemName.General;
        }
    }
}