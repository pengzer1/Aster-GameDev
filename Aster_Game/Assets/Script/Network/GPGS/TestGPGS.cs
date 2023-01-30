using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG.Network.AGGPGS
{
public class TestGPGS : MonoBehaviour
{
        private string log;

        void OnGUI()
        {
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * 3);


            if (GUILayout.Button("ClearLog"))
                log = "";

            if (GUILayout.Button("Login"))
                GPGSSingleton.Inst.Login((success, localUser) =>
                log = $"{success}, {localUser.userName}, {localUser.id}, {localUser.state}, {localUser.underage}");

            if (GUILayout.Button("Logout"))
                GPGSSingleton.Inst.Logout();

            // if (GUILayout.Button("SaveCloud"))
            //     GPGSSingleton.Inst.SaveCloud("mysave", "want data", success => log = $"{success}");

            // if (GUILayout.Button("LoadCloud"))
            //     GPGSSingleton.Inst.LoadCloud("mysave", (success, data) => log = $"{success}, {data}");

            // if (GUILayout.Button("DeleteCloud"))
            //     GPGSSingleton.Inst.DeleteCloud("mysave", success => log = $"{success}");

            // if (GUILayout.Button("ShowAchievementUI"))
            //     GPGSSingleton.Inst.ShowAchievementUI();

            // if (GUILayout.Button("UnlockAchievement_one"))
            //     GPGSSingleton.Inst.UnlockAchievement(GPGSIds.achievement_one, success => log = $"{success}");

            // if (GUILayout.Button("UnlockAchievement_two"))
            //     GPGSSingleton.Inst.UnlockAchievement(GPGSIds.achievement_two, success => log = $"{success}");

            // if (GUILayout.Button("IncrementAchievement_three"))
            //     GPGSSingleton.Inst.IncrementAchievement(GPGSIds.achievement_three, 1, success => log = $"{success}");

            // if (GUILayout.Button("ShowAllLeaderboardUI"))
            //     GPGSSingleton.Inst.ShowAllLeaderboardUI();

            // if (GUILayout.Button("ShowTargetLeaderboardUI_num"))
            //     GPGSSingleton.Inst.ShowTargetLeaderboardUI(GPGSIds.leaderboard_num);

            // if (GUILayout.Button("ReportLeaderboard_num"))
            //     GPGSSingleton.Inst.ReportLeaderboard(GPGSIds.leaderboard_num, 1000, success => log = $"{success}");

            // if (GUILayout.Button("LoadAllLeaderboardArray_num"))
            //     GPGSSingleton.Inst.LoadAllLeaderboardArray(GPGSIds.leaderboard_num, scores =>
            //     {
            //         log = "";
            //         for (int i = 0; i < scores.Length; i++)
            //             log += $"{i}, {scores[i].rank}, {scores[i].value}, {scores[i].userID}, {scores[i].date}\n";
            //     });

            // if (GUILayout.Button("LoadCustomLeaderboardArray_num"))
            //     GPGSSingleton.Inst.LoadCustomLeaderboardArray(GPGSIds.leaderboard_num, 10,
            //         GooglePlayGames.BasicApi.LeaderboardStart.PlayerCentered, GooglePlayGames.BasicApi.LeaderboardTimeSpan.Daily, (success, scoreData) =>
            //         {
            //             log = $"{success}\n";
            //             var scores = scoreData.Scores;
            //             for (int i = 0; i < scores.Length; i++)
            //                 log += $"{i}, {scores[i].rank}, {scores[i].value}, {scores[i].userID}, {scores[i].date}\n";
            //         });

            // if (GUILayout.Button("IncrementEvent_event"))
            //     GPGSSingleton.Inst.IncrementEvent(GPGSIds.event_event, 1);

            // if (GUILayout.Button("LoadEvent_event"))
            //     GPGSSingleton.Inst.LoadEvent(GPGSIds.event_event, (success, iEvent) =>
            //     {
            //         log = $"{success}, {iEvent.Name}, {iEvent.CurrentCount}";
            //     });

            // if (GUILayout.Button("LoadAllEvent"))
            //     GPGSSingleton.Inst.LoadAllEvent((success, iEvents) =>
            //     {
            //         log = $"{success}\n";
            //         foreach (var iEvent in iEvents)
            //             log += $"{iEvent.Name}, {iEvent.CurrentCount}\n";
            //     });

            GUILayout.Label(log);
        }
    }
}