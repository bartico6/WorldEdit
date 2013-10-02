﻿using System;
using Terraria;
using TShockAPI;

namespace WorldEdit.Commands
{
	public class Set : WECommand
	{
		private static string[] SpecialTileNames = { "air", "lava", "honey", "water" };

		private int tile;

		public Set(int x, int y, int x2, int y2, TSPlayer plr, int tile)
			: base(x, y, x2, y2, plr)
		{
			this.tile = tile;
		}

		public override void Execute()
		{
			Tools.PrepareUndo(x, y, x2, y2, plr);
			int edits = 0;
			for (int i = x; i <= x2; i++)
			{
				for (int j = y; j <= y2; j++)
				{
					if (selectFunc(i, j, plr) &&
						((tile >= 0 && (!Main.tile[i, j].active() || Main.tile[i, j].type != tile))
						|| (tile == -1 && (Main.tile[i, j].active() || Main.tile[i, j].liquid != 0))
						|| (tile == -2)
						|| (tile == -3)
						|| (tile == -4)))
					{
						SetTile(i, j, tile);
						edits++;
					}
				}
			}
			ResetSection();

			string tileName = tile < 0 ? SpecialTileNames[-tile - 1] : "tile " + tile;
			plr.SendSuccessMessage("Set tiles to {0}. ({1})", tileName, edits);
		}
	}
}