using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Bugfender))]
[CanEditMultipleObjects]
public class BugfenderCustomEditor : Editor
{
    SerializedProperty AppKey;
    private static GUIStyle header;
    private static GUIContent bugKey;
    private static Texture2D _icon;

    void OnEnable()
    {
        AppKey = serializedObject.FindProperty("APP_KEY");
        header = new GUIStyle()
        {
            fontStyle = FontStyle.Bold
        };
        _icon = Resources.Load<Texture2D>("BugfenderLogo");

        bugKey = new GUIContent();
        bugKey.text = "Key";
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent(_icon));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Bugfender Key", header);
        EditorGUILayout.PropertyField(AppKey, GUIContent.none);

        GUILayout.Space(30f);
        EditorGUILayout.LabelField("How to use Bugfender:", header);
        EditorGUILayout.SelectableLabel("Bugfender.Log(\"BF Initialized\");");
        EditorGUILayout.SelectableLabel("Bugfender.SetDeviceString(\"key\", \"value\");");
        EditorGUILayout.SelectableLabel("Bugfender.SetDeviceString(\"key2\", \"will remove\");");
        EditorGUILayout.SelectableLabel("Bugfender.RemoveDeviceKey(\"key2\");");
        EditorGUILayout.SelectableLabel("Bugfender.SendIssue(\"test\", \"this is a test\");");
        EditorGUILayout.SelectableLabel("Utils.ForceCrash(ForcedCrashCategory.Abort);");

        GUILayout.Space(30f);

   
        EditorGUILayout.LabelField("Documentation:", header);


        if (GUILayout.Button("User Manual", GUILayout.ExpandWidth(true)))
        {
            Application.OpenURL("https://support.bugfender.com/en/articles/118148-can-i-use-bugfender-with-unity");
        }
     
        serializedObject.ApplyModifiedProperties();
    }

}