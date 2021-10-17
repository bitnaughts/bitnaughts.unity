using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    int delay = 0;
    void FixedUpdate()
    {

        if (text.text.Contains("\n")) {
            if (delay < 0) delay++;
            else { text.text = text.text.Substring(text.text.IndexOf("\n") + 1); delay = -30; } 
        }
        else {
            if (delay < 0) delay++;
            else { text.text = ""; delay = -30; }
        }
    }
}
        // case +032: view.text = "\n  ◌ <b>Sensor</b>"; plotter.Focus("Sensor", true); ButtonStyle(8, FontStyle.Bold); break;
        // case +033: view.text = structure.GetComponentToString("Sensor").Replace("◌ <b>Sensor</b>", "<a>◌ <b>Sensor</b></a>"); Button(8, true); break;
        // case +034: view.text = structure.GetComponentToString("Sensor").Replace("⇢ Range", "<a>⇢ Range</a>"); plotter.Select("Sensor"); EnableGameObject("Sensor", true); Button(8, false); ButtonStyle(9, FontStyle.Bold); break;
        // case +035: view.text = structure.GetComponentToString("Sensor"); plotter.Deselect(); tutorial_loaded = false; Button(9, true); break;

        // case +078: view.text = "\n> <b>"    + _add_above     + "</b>\n> "    + _flow_control  + "\n"        + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬"));  Syntax();  Button(0, false);  tutorial_loaded = false; break;
        // case +079: view.text = "\n> <b><a>" + _add_above     + "</a></b>\n> "    + _flow_control  + "\n"    + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬"));  if (tutorial_loaded == false) { tutorial_loaded = true; structure.SetInstructions("Processor","START\nobj Thruster 1\nobj Gimbal 10\n\njum START"); } break;
        // case +080: view.text = "\n> "       + _add_above     + "\n> <b>"    + _flow_control  + "</b>\n"     + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")); Button(6, true); tutorial_loaded = false; break;
        // case +081: view.text = "\n> "       + _add_above     + "\n> <b><a>" + _flow_control  + "</a></b>\n" + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")); SetButtons(_flow_control_menu); Button(6, false); break;
        // case +082: view.text = "\n> <b>"    + _jump_If_Equal + "</b>\n  moves ⇥ Pointer\n"                  + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")); tutorial_loaded = false; break;
        // case +083: view.text = "\n> <b><a>" + _jump_If_Equal + "</a></b>\n  moves ⇥ Pointer\n"              + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")).Replace("jie", "<b><a>jie</a></b>");  if (tutorial_loaded == false) { tutorial_loaded = true; structure.SetInstructions("Processor","START\nobj Thruster 1\nobj Gimbal 10\njie\njum START"); } SetButtons(_operand_menu); Button(4, false); break;
        // case +084: view.text = "\n> <b>"    + _objects2      + "</b>\n  calls ▢ Objects;\n"                 + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")); tutorial_loaded = false; break;
        // case +085: view.text = "\n> <b><a>" + _objects2      + "</a></b>\n  calls ▢ Objects;\n"             + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")).Replace("0", "<b><a>0</a></b>"); SetButtons(structure.GetInteractiveComponents()); Button(4, false); break;
        // case +086: view.text = "\n> <b>"    + _sensor        + "</b>\n  returns ⇢ Range;\n"                 + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")); tutorial_loaded = false; break;
        // case +087: view.text = "\n> <b><a>" + _sensor        + "</a></b>\n  returns ⇢ Range;\n"             + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")).Replace("jie Sensor 0", "jie <b><a>Sensor</a></b> 0"); if (tutorial_loaded == false) { tutorial_loaded = true; structure.SetInstructions("Processor","START\nobj Thruster 1\nobj Gimbal 10\njie Sensor 0\njum START"); } SetButtons(_constant_menu); Button(4, false); break;
        // case +088: view.text = "\n> <b>⇢ 0</b> means:\n  \"nothing detected\";\n"                           + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")); break;
        // case +089: view.text = "\n> <b><a>⇢ 0</a></b> means:\n  \"nothing detected\";\n"                    + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")).Replace("jie Sensor 0 START", "jie Sensor <b><a>0 START</a></b>"); if (tutorial_loaded == false) { tutorial_loaded = true; structure.SetInstructions("Processor","START\nobj Thruster 1\nobj Gimbal 10\njie Sensor 0 START\njum START"); } Button(0, true); break;
        // case +096: view.text = "\n> <b>"    + _add_above + "</b>\n> "     + _objects2 + "\n"                + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")); tutorial_loaded = false;  Button(0, false); Button(1, true); break;
        // case +097: view.text = "\n> <b><a>" + _add_above + "</a></b>\n> " + _objects2 + "\n"                + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")); if (tutorial_loaded == false) { tutorial_loaded = true; structure.SetInstructions("Processor","START\nobj Thruster 1\nobj Gimbal 10\njie Sensor 0 START\nobj Cannon\n\njum START"); } Button(1, false);  break;// structure.AddLine("Processor", -1); break;
        // case +098: view.text = "\n> "       + _add_above + "\n> <b>"      + _objects2  + "</b>\n"           + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")); tutorial_loaded = false; Button(2, true); break;
        // case +099: view.text = "\n> "       + _add_above + "\n> <b><a>"   + _objects2  + "</a></b>\n"       + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")).Replace("obj\n", "<b><a>obj</a></b>\n"); if (tutorial_loaded == false) { tutorial_loaded = true; structure.SetInstructions("Processor","START\nobj Thruster 1\nobj Gimbal 10\njie Sensor 0 START\nobj Cannon\nobj\njum START"); } SetButtons(structure.GetInteractiveComponents()); Button(2, false); break;
        // case +100: view.text = "\n> <b>"    + _gimbal    + "</b>\n  enables ↻ Rotation;\n"                  + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")); tutorial_loaded = false; Button(4, true); break;
        // case +101: view.text = "\n> <b><a>" + _gimbal    + "</a></b>\n  enables ↻ Rotation;\n"              + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")).Replace("obj Gimbal\n", "obj <b><a>Gimbal</a></b>\n"); if (tutorial_loaded == false) { tutorial_loaded = true; structure.SetInstructions("Processor","START\nobj Thruster 1\nobj Gimbal 10\njie Sensor 0 START\nobj Cannon\nobj Gimbal\njum START"); } SetButtons(_constant_menu); Button(4, false); break;
        // case +102: view.text = "\n> <b>Subtract</b> - 10\n  ↻ Rotation:\n"                                  + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")); break;
        // case +103: view.text = "\n> Subtract <b>- 10</b>\n  ↻ Rotation:\n"                                  + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")); tutorial_loaded = false; Button(2, true); break;
        // case +104: view.text = "\n> Subtract <b><a>- 10</a></b>\n  <b>↻ Rotation</b>:\n"                    + structure.GetComponentToString("Processor").Substring(structure.GetComponentToString("Processor").IndexOf("\n  <b>◬")).Replace("obj Gimbal -10", "obj Gimbal <b><a>-10</a></b>"); if (tutorial_loaded == false) { tutorial_loaded = true; structure.SetInstructions("Processor","START\nobj Thruster 1\nobj Gimbal 10\njie Sensor 0 START\nobj Cannon\nobj Gimbal -10\njum START"); } Button(2, false); break;

/*
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┗ <b>□ Structures</b>";
            //     buttons[0].transform.GetChild(0).gameObject.SetActive(true);
            //     buttons[1].transform.GetChild(0).gameObject.SetActive(true);
            //     buttons[2].transform.GetChild(0).gameObject.SetActive(true);
            //     buttons[3].transform.GetChild(0).gameObject.SetActive(true);
            //     buttons[4].transform.GetChild(0).gameObject.SetActive(true);
            //     break;
            // case 13:
            //     buttons[0].transform.GetChild(0).gameObject.SetActive(false);
            //     buttons[1].transform.GetChild(0).gameObject.SetActive(false);
            //     buttons[2].transform.GetChild(0).gameObject.SetActive(false);
            //     buttons[3].transform.GetChild(0).gameObject.SetActive(false);
            //     buttons[4].transform.GetChild(0).gameObject.SetActive(false);
            //     break;
            // case 14:
            //     //TODO: select processor on example ship... focus(processor1)
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┗ □ Structures\n    ┗ <b>" + _processor + "</b>";
            //     buttons[0].transform.GetChild(0).gameObject.SetActive(true);
            //     break;
            // case 15:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┗ □ Structures\n    ┗ " + _processor + "\n   <b>" + _processor_description + "</b>";
            //     buttons[0].transform.GetChild(0).gameObject.SetActive(false);
            //     break;
            // case 16:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┗ □ Structures\n    ┠ " + _processor + "\n    ┗ <b>" + _cache + "</b>";
            //     buttons[1].transform.GetChild(0).gameObject.SetActive(true);
            //     break;
            // case 17:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┗ □ Structures\n    ┠ " + _processor + "\n    ┗ " + _cache + "\n   <b>" + _cache_description + "</b>";
            //     buttons[1].transform.GetChild(0).gameObject.SetActive(false);
            //     break;
            // case 18:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┗ □ Structures\n    ┠ " + _processor + "\n    ┠ " + _cache + "\n    ┗ <b>" + _bulkhead + "</b>";
            //     buttons[2].transform.GetChild(0).gameObject.SetActive(true);
            //     break;
            // case 19:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┗ □ Structures\n    ┠ " + _processor + "\n    ┠ " + _cache + "\n    ┗ " + _bulkhead + "\n   <b>" + _bulkhead_description + "</b>";
            //     buttons[2].transform.GetChild(0).gameObject.SetActive(false);
            //     break;
            // case 20:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┗ □ Structures\n    ┠ " + _processor + "\n    ┠ " + _cache + "\n    ┠ " + _bulkhead + "\n    ┗ <b>" + _girder + "</b>";
            //     buttons[3].transform.GetChild(0).gameObject.SetActive(true);
            //    break;
            // case 21:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┗ □ Structures\n    ┠ " + _processor + "\n    ┠ " + _cache + "\n    ┠ " + _bulkhead + "\n    ┗ " + _girder + "\n   <b>" + _girder_description + "</b>";
            //     buttons[3].transform.GetChild(0).gameObject.SetActive(false);
            //     break;
            // case 22:
            //     buttons[4].transform.GetChild(0).gameObject.SetActive(true);
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┗ □ Structures\n    ┠ " + _processor + "\n    ┠ " + _cache + "\n    ┠ " + _bulkhead + "\n    ┠ " + _girder + "\n    ┗ <b>" + _gimbal + "</b>";
            //     break;
            // case 23:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┗ □ Structures\n    ┠ " + _processor + "\n    ┠ " + _cache + "\n    ┠ " + _bulkhead + "\n    ┠ " + _girder + "\n    ┗ " + _gimbal + "\n   <b>" + _gimbal_description + "</b>";
            //     buttons[4].transform.GetChild(0).gameObject.SetActive(false);
            //     break;
            // case 24:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  └ □ Structures\n    ├ " + _processor + "\n    ├ " + _cache + "\n    ├ " + _bulkhead + "\n    ├ " + _girder + "\n    └ " + _gimbal;
            //     break;
            // case 25:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┠ □ Structures\n  ┗ <b>○ Modules</b>";
            //     buttons[5].transform.GetChild(0).gameObject.SetActive(true);
            //     buttons[6].transform.GetChild(0).gameObject.SetActive(true);
            //     buttons[7].transform.GetChild(0).gameObject.SetActive(true);
            //     buttons[8].transform.GetChild(0).gameObject.SetActive(true);
            //     break;
            // case 26:
            //     buttons[5].transform.GetChild(0).gameObject.SetActive(false);
            //     buttons[6].transform.GetChild(0).gameObject.SetActive(false);
            //     buttons[7].transform.GetChild(0).gameObject.SetActive(false);
            //     buttons[8].transform.GetChild(0).gameObject.SetActive(false);
            //     break;
            // case 27:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┠ □ Structures\n  ┗ ○ Modules\n    ┗ <b>" + _cannon + "</b>";
            //     buttons[5].transform.GetChild(0).gameObject.SetActive(true);
            //     break;
            // case 28:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┠ □ Structures\n  ┗ ○ Modules\n    ┗ " + _cannon + "\n   <b>" + _cannon_description + "</b>";
            //     buttons[5].transform.GetChild(0).gameObject.SetActive(false);
            //     break;
            // case 29:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┠ □ Structures\n  ┗ ○ Modules\n    ┠ " + _cannon + "\n    ┗ <b>" + _thruster + "</b>";
            //     buttons[6].transform.GetChild(0).gameObject.SetActive(true);
            //     break;
            // case 30:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┠ □ Structures\n  ┗ ○ Modules\n    ┠ " + _cannon + "\n    ┗ " + _thruster + "\n   <b>" + _thruster_description + "</b>";
            //     buttons[6].transform.GetChild(0).gameObject.SetActive(false);
            //     break;
            // case 31:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┠ □ Structures\n  ┗ ○ Modules\n    ┠ " + _cannon + "\n    ┠ " + _thruster + "\n    ┗ <b>" + _booster + "</b>";
            //     buttons[7].transform.GetChild(0).gameObject.SetActive(true);
            //     break;
            // case 32: 
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┠ □ Structures\n  ┗ ○ Modules\n    ┠ " + _cannon + "\n    ┠ " + _thruster + "\n    ┗ " + _booster + "\n   <b>" + _booster_description + "</b>";
            //     buttons[7].transform.GetChild(0).gameObject.SetActive(false);
            //     break;
            // case 33:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┠ □ Structures\n  ┗ ○ Modules\n    ┠ " + _cannon + "\n    ┠ " + _thruster + "\n    ┠ " + _booster + "\n    ┗ <b>" + _sensor + "</b>";
            //     buttons[8].transform.GetChild(0).gameObject.SetActive(true);
            //     break;
            // case 34:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ┠ □ Structures\n  ┗ ○ Modules\n    ┠ " + _cannon + "\n    ┠ " + _thruster + "\n    ┠ " + _booster + "\n    ┗ " + _sensor + "\n   <b>" + _sensor_description + "</b>";
            //     buttons[8].transform.GetChild(0).gameObject.SetActive(false);
            //     break;
            // case 35:
            //     view.text = "\n> △ Machines are\n  ▢ Object-oriented\n  ├ □ Structures\n  └ ○ Modules\n    ├ " + _cannon + "\n    ├ " + _thruster + "\n    ├ " + _booster + "\n    └ " + _sensor;
            //     break;
*/