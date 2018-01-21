using UnityEngine;
using UnityEditor;
using System.IO;

namespace Area730.Notifications.IOS
{

    public class IOSNotificationsWindow : EditorWindow
    {

        #region Vars

        [SerializeField]
        private IOSDataHolder      _dataHolder;
        private static GUISkin  _customSkin             = null;
        private Vector2         _scrollPosition         = Vector2.zero;
        private bool            _showHelp               = false;
        private bool            _showNotificationList   = false;
        private bool            _showSettings           = true;

        #endregion

        #region Editor window config

        void OnEnable()
        {
            if (!Directory.Exists(IOSEditorConstants.RESOURCES_PATH))
            {
                Directory.CreateDirectory(IOSEditorConstants.RESOURCES_PATH);
            }

            _dataHolder = AssetDatabase.LoadAssetAtPath(IOSEditorConstants.SAVE_PATH, typeof(IOSDataHolder)) as IOSDataHolder;

            if (_dataHolder == null)
            {
                _dataHolder = CreateInstance<IOSDataHolder>();
                AssetDatabase.CreateAsset(_dataHolder, IOSEditorConstants.SAVE_PATH);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            _customSkin = EditorGUIUtility.Load(IOSEditorConstants.EDITOR_SKIN_FILENAME) as GUISkin;
        }

        void OnDestroy()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem(IOSEditorConstants.EDITOR_WINDOW_MENU_NAME)]
        public static void ShowWindow()
        {
            IOSNotificationsWindow window = (IOSNotificationsWindow)EditorWindow.GetWindow(typeof(IOSNotificationsWindow));
            window.title = IOSEditorConstants.EDITOR_WINDOW_TITLE;
            window.Show();
        }

        #endregion

        void OnGUI()
        {
            GUI.skin        = _customSkin;
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            GUIStyle labelStyle = GUI.skin.GetStyle(IOSEditorConstants.STYLE_TEXT_FIELD_LABEL);

            EditorGUILayout.LabelField("Notification settings", GUI.skin.label);

            // Help section
            _showHelp = EditorGUILayout.Foldout(_showHelp, "Help", IOSEditorUtils.FoldoutStyle());
            if (_showHelp)
            {
                GUIStyle style = GUI.skin.GetStyle("HelpButtons");

                EditorGUILayout.LabelField("Email: support@area730.com", labelStyle);

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Docs", style))
                {
                    Help.BrowseURL(IOSEditorConstants.URL_DOCUMENTATION);
                }

                if (GUILayout.Button("Questions?", style))
                {
                    Help.BrowseURL(IOSEditorConstants.URL_QUESTIONFORM);
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
//
//                if (GUILayout.Button("Small icon generator", GUI.skin.button, GUILayout.Height(35)))
//                {
//                    Help.BrowseURL(EditorConstants.URL_SMALL_ICON_GEN);
//                }
//
//                if (GUILayout.Button("Large icon generator", GUI.skin.button, GUILayout.Height(35)))
//                {
//                    Help.BrowseURL(EditorConstants.URL_LARGE_ICON_GEN);
//                }

            }

//            EditorGUILayout.Space();
//
//            // Settings section
//            _showSettings = EditorGUILayout.Foldout(_showSettings, "Settings", EditorUtils.FoldoutStyle());
//            if (_showSettings)
//            {
//                EditorGUILayout.Space();
//                EditorGUILayout.BeginHorizontal();
//                EditorGUILayout.LabelField(new GUIContent("Untiy class", "Class that extends com.unity3d.player.UnityPlayerNativeActivity"), labelStyle, GUILayout.Width(100));
//                _dataHolder.unityClass = EditorGUILayout.TextField(_dataHolder.unityClass);
//                EditorGUILayout.EndHorizontal();
//            }

            EditorGUILayout.Space();

            // Notification list
            _showNotificationList = EditorGUILayout.Foldout(_showNotificationList, "Notification list", IOSEditorUtils.FoldoutStyle());
            if (_showNotificationList)
            {
                IOSEditorUtils.DrawDataHolder(_dataHolder);
            }

            GUILayout.EndScrollView();

            if(GUI.changed)
            {
                EditorUtility.SetDirty(_dataHolder);
                Repaint();
            }
        }

    }

}