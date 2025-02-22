using TheDepths.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace TheDepths.NPCs
{
    public class TheDepthsGlobalNPC : GlobalNPC
	{
	    public override bool InstancePerEntity => true;

	    public bool merPoison;
		public bool slowWater;
		public bool merBoiling;
		public int MercuryNPCTimer;
		
		public override void ResetEffects(NPC npc) {
			merPoison = false;
			slowWater = false;
			merBoiling = false;
		}
		
	    public override void DrawEffects(NPC npc, ref Color drawColor) {
			if (merPoison) {
				if (Main.rand.Next(4) < 3) {
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<MercuryFire>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					if (Main.rand.NextBool(4)) {
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
			}
			if (slowWater) {
				if (Main.rand.Next(4) < 3) {
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<SlowingWaterFire>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					if (Main.rand.NextBool(4)) {
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
			}
			if (merBoiling) {
				if (Main.rand.Next(4) < 3) {
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<MercuryFire>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					if (Main.rand.NextBool(4)) {
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
				Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
			}
		}
		
		public override void UpdateLifeRegen(NPC npc, ref int damage) {
			if (slowWater && !npc.boss)
			{
				npc.velocity.X = 0f;
				npc.velocity.Y = 0f;
				if (npc.velocity.Y > 0f)
				{
					npc.velocity.Y = 0f;
				}
			}
			if (merBoiling)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				MercuryNPCTimer++;
				npc.lifeRegen -= Utils.Clamp(MercuryNPCTimer / 60, 0, 10);
			}
			if (!merBoiling && MercuryNPCTimer >= 1)
			{
				MercuryNPCTimer--;
			}
		}

        public override void ModifyShop(NPCShop shop)
		{
			var depthsWorld = new Condition("Mods.TheDepths.DepthsBiome", () => TheDepthsWorldGen.depthsorHell);
			if (shop.NpcType == NPCID.Clothier)
			{
				shop.InsertAfter(ItemID.PlumbersShirt, ModContent.ItemType<Items.Armor.PurplePlumbersShirt>(), Condition.MoonPhaseFull, depthsWorld);
				shop.InsertAfter(ItemID.PlumbersPants, ModContent.ItemType<Items.Armor.PurplePlumbersPants>(), Condition.MoonPhaseFull, depthsWorld);
				if (shop.TryGetEntry(ItemID.PlumbersShirt, out NPCShop.Entry entry))
				{
					entry.Disable();
				}
				if (shop.TryGetEntry(ItemID.PlumbersPants, out NPCShop.Entry entry2))
				{
					entry2.Disable();
				}
			}
			if (shop.NpcType == NPCID.Dryad)
            {
				shop.InsertAfter(ItemID.FireBlossomPlanterBox, ModContent.ItemType<Items.Placeable.ShadowShrubPlanterBox>(), Condition.Hardmode, depthsWorld);
			}
			if (shop.NpcType == NPCID.BestiaryGirl)
            {
				shop.InsertAfter(ItemID.WorldGlobe, ModContent.ItemType<Items.CoreGlobe>(), Condition.Hardmode);
			}
		}
	}
}