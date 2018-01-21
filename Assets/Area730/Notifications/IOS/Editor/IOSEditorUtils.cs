using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

namespace Area730.Notifications.IOS
{
    /// <summary>
    /// Some utils to draw notification editor window
    /// </summary>
    public class IOSEditorUtils
    {

        /// <summary>
        /// Android allowed filename regex
        /// </summary>
        private static Regex regex = new Regex("^[a-z0-9_.]+$");
		private static int selecteInterval = 0;

        /// <summary>
        /// Draws the dataholder editor ui
        /// </summary>
        /// <param name="dataHolder">Dataholder to draw</param>
        public static void DrawDataHolder(IOSDataHolder dataHolder)
        {
            EditorGUILayout.Space();

            foreach (var notif in dataHolder.notifications)
            {
                DrawNotification(notif);

                GUIStyle deleteBtnStyle = GUI.skin.GetStyle(IOSEditorConstants.STYLE_DELETE_BTN);

                if (GUILayout.Button("Delete", deleteBtnStyle, GUILayout.Width(100)))
                {
                    dataHolder.notifications.Remove(notif);
                    GUI.changed = true;
                    break;
                }

                EditorGUILayout.Space();
            }

            if (GUILayout.Button("Add Notification"))
            {
                dataHolder.notifications.Add(new IOSNotificationInstance());
            }
        }

        /// <summary>
        /// Draws the notification editor ui
        /// </summary>
        /// <param name="notif">Notification to draw</param>
        public static void DrawNotification(IOSNotificationInstance notif)
        {
            GUIStyle labelStyle = GUI.skin.GetStyle(IOSEditorConstants.STYLE_TEXT_FIELD_LABEL);

            // draw headline
            EditorGUILayout.LabelField(IOSEditorConstants.NOTIFICATION_HEADLINE, GUI.skin.label);


            DrawTextField(labelStyle, "Name", ref notif.name);

            DrawIntField(labelStyle, "ID", "Notification id", ref notif.id);


            DrawTextField(labelStyle, "Title", ref notif.title);
            if (notif.title == null || notif.title.Length == 0)
            {
                EditorGUILayout.HelpBox("Titile can't be empty", MessageType.Error);
            }
            DrawTextField(labelStyle, "Body", ref notif.body);
            if (notif.body == null || notif.body.Length == 0)
            {
                EditorGUILayout.HelpBox("Body can't be empty", MessageType.Error);
            }

			DrawIntField(labelStyle, "Badges", "badge number", ref notif.number);
            
            DrawToggleField(labelStyle, "Is repeating?", IOSEditorConstants.TOOLTIP_IS_REPEATING, ref notif.isRepeating);

            // If Repeating
            if (notif.isRepeating)
            {

				GUILayout.BeginVertical("Box");
                EditorGUILayout.LabelField(new GUIContent("Interval:", IOSEditorConstants.TOOLTIP_INTERVAL), labelStyle, GUILayout.Width(100));

                EditorGUI.indentLevel += 2; ;

				var text = new string[] { "minute", "hour", "day", "months", "year" };
				selecteInterval = GUILayout.SelectionGrid(selecteInterval, text, 2, EditorStyles.radioButton);

				notif.interval = (IntervalUnits)selecteInterval;


                EditorGUI.indentLevel -= 2;
				GUILayout.EndVertical();

            }

            // Delay
            EditorGUILayout.LabelField(new GUIContent("Delay", IOSEditorConstants.TOOLTIP_DELAY), labelStyle, GUILayout.Width(100));

            EditorGUI.indentLevel += 2;
            DrawIntField(labelStyle, "Hours", "Hours before the norification will be shown", ref notif.delayHours, GUILayout.Width(100));
            DrawIntField(labelStyle, "Minutes", "Minutes before the norification will be shown", ref notif.delayMinutes, GUILayout.Width(100));
            DrawIntField(labelStyle, "Seconds", "Seconds before the norification will be shown", ref notif.delaySeconds, GUILayout.Width(100));
            EditorGUI.indentLevel -= 2;

            //
            
            DrawToggleField(labelStyle, "Default Sound", IOSEditorConstants.TOOLTIP_DEFAULT_SOUND, ref notif.defaultSound);

            if (!notif.defaultSound)
            {
                HandleAudioField(labelStyle, "Sound clip", ref notif.soundFile);
            }

            
        }

 

        /// <summary>
        /// Draws audio field and copies the clip into res/raw folder
        /// </summary>
        private static void HandleAudioField(GUIStyle style, string title, ref AudioClip clip)
        {
            AudioClip oldClip = clip;

            // draw audio field
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Sound file", style, GUILayout.Width(100));
            clip = (AudioClip)EditorGUILayout.ObjectField(clip, typeof(AudioClip), false);
            EditorGUILayout.EndHorizontal();

            if (oldClip != clip)
            {
                if (clip == null)
                {
                    return;
                }

                if (!Directory.Exists(IOSEditorConstants.PATH_TO_RAW))
                {
                    EditorUtility.DisplayDialog("ERROR", "Path " + IOSEditorConstants.PATH_TO_RAW + " doesn't exist. Changes weren't applied", "OK");
                    // revert changes
                    clip = oldClip;
                    return;
                }

                string newClipPath          = AssetDatabase.GetAssetPath(clip);
                string newClipFileName      = Path.GetFileName(newClipPath);

                string iosNewClipPath   	= Path.Combine(IOSEditorConstants.PATH_TO_RAW, newClipFileName);
				string fakePath				= Path.Combine(IOSEditorConstants.PATH_TO_RAW_FAKE, newClipFileName);

                // check is filename is allowed
                if (!regex.IsMatch(newClipFileName))
                {
                    EditorUtility.DisplayDialog("ERROR", "New sound wasn't applied. Android allows only names that contain [a-z0-9_.]. Please rename your file and try again.", "OK");
                    // revert changes
                    clip = oldClip;
                    return;
                }

                // copy if file not exist
                if (!File.Exists(iosNewClipPath))
                {
                    File.Copy(newClipPath, iosNewClipPath);
					File.Copy(newClipPath, fakePath);
                    AssetDatabase.Refresh();
                }

                // link clip to file in res/raw folder
                AudioClip rawClip = AssetDatabase.LoadAssetAtPath(fakePath, typeof(AudioClip)) as AudioClip;
                clip = rawClip;

            }
        }

        //==================================================================================

        private static GUILayoutOption miniButtonWidth = GUILayout.Width(20f);

        /// <summary>
        /// Returns the GUIStyle for Foldout element
        /// </summary>
        /// <returns></returns>
        public static GUIStyle FoldoutStyle()
        {
            GUIStyle labelStyle = GUI.skin.customStyles[2];
            GUIStyle s = EditorStyles.foldout;
            s.font = labelStyle.font;
            s.fontSize = 12;
            return s;
        }

        public static void DrawTextField(GUIStyle style, string label, ref string var)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, style, GUILayout.Width(100));
            var = EditorGUILayout.TextField(var);
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawObjectField(GUIStyle style, string label, ref Texture2D var)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, style, GUILayout.Width(100));
            var = (Texture2D)EditorGUILayout.ObjectField(var, typeof(Texture2D), false);
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawColorField(GUIStyle style, string label, ref Color var, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, style, GUILayout.Width(100));
            var = EditorGUILayout.ColorField(var, options);
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawToggleField(GUIStyle style, string label, string tooltip, ref bool var)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent(label, tooltip), style, GUILayout.Width(100));
            var = EditorGUILayout.Toggle(var);
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawIntField(GUIStyle style, string label, string tooltip, ref int var, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent(label, tooltip), style, GUILayout.Width(100));
            var = EditorGUILayout.IntField(var, options);
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawLongField(GUIStyle style, string label, string tooltip, ref long var, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent(label, tooltip), style, GUILayout.Width(100));
            var = EditorGUILayout.IntField((int)var, options);
            EditorGUILayout.EndHorizontal();
        }


    }
}
