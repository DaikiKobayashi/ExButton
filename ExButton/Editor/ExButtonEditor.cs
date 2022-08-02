using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Linq;
using UnityEditor.UI;
using System.Reflection;
using System;

namespace ExTools
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ExButton))]
    public class ExButtonEditor : SelectableEditor
    {

    }
}