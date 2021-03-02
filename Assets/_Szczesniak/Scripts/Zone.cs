using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Szczesniak {
    public class Zone : SlipstreamJumper.Zone {

        /// <summary>
        /// This is used to be able to know who owns this scene, what the name of the game is called, and the scene file name is.
        /// </summary>
        new public static ZoneInfo info = new ZoneInfo() {
            // Information on the game name, developer, and the scene file.
            zoneName = "Mayhem Buggy",
            creator = "Gavin Szczesniak",
            sceneFile = "Zone_Szczesniak"
        };

        // Singleton
        public static Zone main;

        public AABB player;

        // one variable to hold all of the platforms:
        private List<AABB> platforms = new List<AABB>();

        // one variable to hold all of the powerups
        public List<AABB> powerups = new List<AABB>();

        // Runs once
        private void Awake() {
            if (main != null) { // singleton already exist...
                Destroy(gameObject);
            } else {
                main = this;
            }
        }

        // When destroyed
        private void OnDestroy() {
            if (main == this) main = null;
        }

        /// <summary>
        ///  updates last compared to the other updates
        /// </summary>
        void LateUpdate() {

            if (!player) return; // no player, don't do collision detection

            PlayerMovement pm = player.GetComponent<PlayerMovement>();

            // checking collision between PLAYER and all PLATFORMS:
            foreach(AABB box in platforms) {
                if (player.OverlapCheck(box) && pm.checkYVelocity < .5f) {
                    pm.ApplyFix(player.FindFix(box));
                }
            }

            // checking collision between PLAYER and all OVERLAP-OBJECTS:
            foreach(AABB power in powerups)
            {
                if (player.OverlapCheck(power)) {
                    // player collides with powerup!
                    // do something...

                    OverlapObject oo = power.GetComponent<OverlapObject>();
                    if (oo) {
                        oo.OnOverlap(pm);
                    }
                }
            }
           
            /*
            if (player.OverlapCheck(floor)) {
                print("Collision");
                Vector3 fix = player.FindFix(floor);

                player.GetComponent<PlayerMovement>().ApplyFix(fix);

            }
            */

        }

        /// <summary>
        /// This function add a platform to our big list of platforms
        /// </summary>
        /// <param name="platform"></param>
        public void AddPlatform(AABB platform)
        {
            platforms.Add(platform);
        }

        /// <summary>
        /// This function removes a platform to our big list of platforms
        /// </summary>
        /// <param name="platform"></param>
        public void RemovePlatform(AABB platform)
        {
            platforms.Remove(platform);
        }
    }
}