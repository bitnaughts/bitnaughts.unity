using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Video;

public class Launch : MonoBehaviour
{
    public VideoPlayer intro_1, intro_2, intro_3, intro_4, tv, war, war_video;
    public GameObject StructurePrefab;
    GameObject structure_prefab;
    public AudioSource audio;
    public Sprite form_load, form_launch;
    public Sprite arrow_load, arrow_exit;

    public GameObject enemy, enemy2;
    public Camera map_camera;
    public Image form, arrow;
    public Text left_title, right_title, view, debug, debugBack, placeholder;
    public WebGLNativeInputField input;
    public Button[] buttons;

    public GameObject map, canvas;
    float map_zoom = 2;
    public PlotterController plotter;
    public StructureController structure;
    ProcessorController processor;
    bool flip;
    float button_size = .4f;
    bool launched = false, once = false;
    bool launching = false;
    string operation_button = "";
    int number_operands = 0;
    const string code = "2AFOlVOsSaX8Cljp03uUIXt0d64pSQlh3DAVMHepbbtL6apFXjMRfA==";
    Vector2 screen_size;
    float about_timer = 1000f, tutorial_timer = -1f;
    public GameObject _Processor, _Cache, _Girder, _Gimbal, _Bulkhead, _Cannon, _Thruster, _Booster, _Sensor;
    const string _campaign = "≛ Missions", _location = "✵ Location", _line = "", _metrics = "⊞ Metrics", _logs = "⊟ Logs", _editor = "☄ BitNaughts", _monitor = "☄ Monitor",//☍ Editor"✯≐
    _launch = "※ Launch", _settings="⁝ Settings", _audio_up="+ Audio",_audio_down="- Audio", _reorient="> Reorient", _flip="> Flip", _font_size_up="+ Font",_font_size_down="- Font", _button_size_up="+ Buttons",_button_size_down="- Buttons",_debug_mode="$ Debug",
        _about = "≟ About", _tutorial = "? Tutorial", _register = "@ Register", _sign_in = "@", _map = "⸸ Map", _blueprint = "▦ Blueprint", _select = "> Select", _new = "≗ New", _save = "≙ Save", _load = "≚ Load", _leaderboard = "≜ Database", _menu = "> Menu", _objects = "▢ Objects", _objects2 = "⊡ Function",
         //_replicator = " Replicator",
        _processor = "▥ Processor", _processor_description="◬ executes code",  
        _cache = "▤ Cache", _cache_description="≣ stores data", 
        _girder = "▨ Girder", _girder_description="☍ connects objects", 
        // _grid = "▧ Grid"
        _gimbal = "▣ Gimbal", _gimbal_description="↺ rotates objects", 
        _bulkhead = "▩ Bulkhead",_bulkhead_description="♨ stores fuel", 
        _cannon = "◍ Cannon", _cannon_description="⥉ shoots w/ mass",
        _thruster = "◉ Thruster", _thruster_description="⇊ burns fuel", 
        _booster = "◎ Booster", _booster_description="⇊ burns fuel", 
        _sensor = "◌ Sensor", _sensor_description="⇢ measures ranges", 
        _zoom_in = "⇲ Zoom in", _zoom_out = "⇱ Zoom out", _ok="☇ Ok",
        
        _scroll_up = "↥ Scroll", _scroll_down = "↧ Scroll", _add_above = "⨥ New Line", _add_below = "+ Add Below", _back = "< Back", _delete = "⨪ Delete Line",
        _syntax = "◬ Syntax", _arithmetic = "∓ Arithmetic", _flow_control = "⋚ Flow Control", _boolean = "◑ Boolean", _trigonometry = "∡ Trigonometry", //⊶
        _jumpLabel = "≝ Jump Label", _jump = "⇥ Jump", _jump_If_Equal = "= Jump Equal", _jump_If_Not_Equal = "≠ Jump Not", _jump_If_Greater = "≩ Jump More", _jump_If_Less = "≨ Jump Less",  
        _add = "+ Add", _subtract = "- Sub", _absolute = "∥ Absolute", _multiply = "× Multiply", _divide = "÷ Divide", _modulo = "% Modulo", _exponential = "^ Exponential", _root = "√ Root", 
        _sine = "∿ Sin", _cosine = "∿ Cos", _tangent = "∠ Tan", _secant = "∡ Sec", _cosecant = "∡ Cosec", _cotangent = "∡ Cotan", _arcsine = "∢ Arcsin", _arccosine = "∢ Arccos", _arctangent = "∢ Arctan",
        _function = "□ Function", _mark = "⊞ Mark", _log = "⊟ Log", _ping = "> Ping", _enter = "> Enter", 
        _set = "> Set", _and = "⋀ And", _or = "⋁ Or", _xor = "⊻ Xor", _nand = "⊼ Nand", _nor = "⊽ Nor",
        UpCarat = "^ ", DownCarat = "v ", MinusCarat = "- ", PlusCarat = "+ ", Carat = "> ", _constant = "> Constant", _variable = "> Variable", _user_input = "> User Input", _random = "> Random";
    // Vector2d[] launch_locations = { new Vector2d(2.37, 16.06), new Vector2d(51.72472, 94.44361), new Vector2d(-19.23, 133.21), new Vector2d(48.30, 23.23), new Vector2d(39.833, -98.583), new Vector2d(-15.27, -55.45)}; //(48° 21' 19" N 99° 59' 57" W) and the geographical center of South America (15° 27′ 39″ S, 55° 45′ 0″ W).
    string[] settings_options = { _audio_up, _audio_down, _font_size_up, _font_size_down, _button_size_up, _button_size_down, _flip, _reorient, _debug_mode, _back },
        _launch_menu = { "Africa", "Asia", "Australia", "Europe", "N. America", "S. America", _zoom_in, _zoom_out, _settings, _back }, //_campaign, _location, _line,  _logs, _metrics,  _objects, _syntax, _line, _settings, _back },
        _launched_menu = {_about, _campaign, _logs, _metrics, _objects, _syntax, _zoom_in, _zoom_out, _settings, _back },
        _missions_menu = { _objects, _logs, _metrics, _syntax, _objects2, _boolean, _arithmetic, _flow_control, _trigonometry, _back },
        _design_menu = { _about, _campaign, _load, _save, _objects, _syntax, _zoom_in, _zoom_out, _settings, _launch },
        _objects_menu = { _processor, _cache, _bulkhead, _girder, _gimbal, _cannon, _thruster, _booster, _sensor, _back },
        _syntax_menu = { _add_above, _objects2, _boolean, _arithmetic, _flow_control, _trigonometry, _scroll_up, _scroll_down, _delete, _back },
        _arthimetic_menu = { _set, _add, _subtract, _absolute, _multiply, _divide, _modulo, _exponential, _root, _back },
        _flow_control_menu = { _jumpLabel, _jump, _jump_If_Equal, _jump_If_Not_Equal, _jump_If_Greater, _jump_If_Less, _line, _line, _line, _back },
        _boolean_menu = { _set, "! Not", _and, _or, _xor, _nand, _nor, _line, _line, _back },
        _trigonometry_menu = { _sine, _cosine, _tangent, _secant, _cosecant, _cotangent, _arcsine, _arccosine, _arctangent, _back },
        _operand_menu = {_constant, _variable, _random, _line, _line, _line, _line, _line, _line, _back},
        _variable_menu = {_ok, _line, _line, _line, _line, _line, _line, _line, _line, _back},
        _jump_label_menu = {_ok, _line, _line, _line, _line, _line, _line, _line, _line, _back},
        _constant_menu = {_enter, "+ 100", "+ 10.", "+ 1.0", "+ 0.1", "- 0.1", "- 1.0", "- 10.", "- 100", _back};
    string[][] continent_cities = {
        new string[]{"Lagos", "Cairo", "Kinshasa", "Alexandria", "Abidjan", "Johannesburg", "Dar es Salaam", "Casablanca", "Cape Town", _back},
        new string[]{"Tokyo", "Jakarta", "Delhi", "Mumbai", "Seoul", "Shanghai", "Manila", "Karachi", "Beijing", _back},
        new string[]{"Sydney", "Melbourne", "Brisbane", "Perth", "Adelaide", "Canberra", "Hobart", "Darwin", "Outback", _back},
        new string[]{"Istanbul", "Moscow", "London", "Saint Petersburg", "Berlin", "Madrid", "Kyiv", "Rome", "Bucharest", _back},
        new string[]{"Mexico City", "New York", "Los Angeles", "Toronto", "Chicago", "Houston", "Havana", "Montreal", "Seattle", _back},
        new string[]{"Sao Paulo", "Lima", "Bogota", "Rio de Janeiro", "Santiago", "Caracas", "Buenos Aires", "Salvador", "Brasilia", _back}
    };
    string menu_description = "\n> Hello World!";
    string design_description = "\n> ";//"\n> New?\n- <b>Tutorial</b>\n\n> Design ship:\n- <b>Objects</b>\n\n> Code ship:\n- <b>Syntax</b>\n\n> Telemetry:\n- <b>Metrics</b>\n- <b>Logs</b>\n\n> Compete:\n- <b>Launch</b>";
    string launch_description = "\n> Select continent\n  or <b>Input</b> any\n  location;";
    string launched_description = "\n> Enter <b>Input</b>\n  to launch;";
    string continent_select_description = "\n> Select city\n  or <b>Input</b> any\n  location;";

    const string default_author = "botnaughts@gmail.com";
    string author;
    Rect[] camera_formats = new Rect[] { new Rect(.5f, 0, .5f, 1), new Rect(0, 0, .5f, 1), new Rect(0, .5f, 1, .5f), new Rect(0, 0, 1, .5f)};
    Rect[] canvas_formats = new Rect[] { new Rect(0, 0, .5f, 1), new Rect(.5f, 0, 1, 1), new Rect(0, 0, 1, .5f), new Rect(0, .5f, 1, 1)};
    Rect[] canvas_padding = new Rect[] { new Rect(18, 30, 0, 19), new Rect(0, 30, 11, 19), new Rect(18, 0, 11, 19), new Rect(18, 30, 11, 0)};
    int format = 0;
    void Start()
    {
        input.contentType = InputField.ContentType.Alphanumeric;
        Design();
        author = default_author;
        screen_size = new Vector2(Screen.width, Screen.height);
        Format(); Format();
        Debug("");
    }
    void SetButtons(string[] menu) {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < menu.Length && menu[i] != "" && menu[i] != null) {
                buttons[i].transform.GetChild(1).GetComponent<Text>().text = menu[i];
            } else {
                buttons[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
            buttons[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public string GetActiveText() {
        for (int i = 0; i < buttons.Length; i++) {
            if (buttons[i].transform.GetChild(0).gameObject.activeSelf) {
                return buttons[i].transform.GetChild(1).GetComponent<Text>().text;
            }
        }
        return "";
    }
    public int GetActiveButton() {
        for (int i = 0; i < buttons.Length; i++) {
            if (buttons[i].transform.GetChild(0).gameObject.activeSelf) {
                return i;
            }
        }
        return -1;
    }

    void Design() 
    {
        saving = false; loading = false; registering = false;
        form.sprite = form_load;
        arrow.sprite = arrow_load;
        left_title.text = _editor;
        right_title.text = _blueprint;
        view.text = design_description;
        map_camera.gameObject.SetActive(true);
        map_camera.backgroundColor = new Color(15/255f, 60/255f, 80/255f);
        map.SetActive(false);
        input.gameObject.SetActive(false);
        EnableGameObject("7", true);
        EnableGameObject("37", true);
        plotter.GetComponent<SpriteRenderer>().enabled = true;
        SetButtons(_design_menu);
    }
    void ObjectMenu() {
        left_title.text = _objects; SetButtons(_objects_menu);
    }

    void Syntax() 
    {
        left_title.text = _syntax;
        right_title.text = _blueprint;
        operation_button = "";
        number_operands = 0;
        input.gameObject.SetActive(false);
        SetButtons(_syntax_menu);
    }

    float time = 0;

    void FixedUpdate() 
    {
        int longest_line = -1;
        foreach (var line in view.text.Split('\n')) {
            int line_length = line.Replace("<i>", "").Replace("</i>", "").Replace("<b>", "").Replace("</b>", "").Length;
            if (line_length > longest_line) longest_line = line_length;
        }
        view.GetComponent<RectTransform>().sizeDelta = new Vector2(longest_line * 8.5f, view.text.Split('\n').Length * 16);
    }
    bool made_ship = false, launched_flag = false;
    float launch_timer = 0;
    void Update()
    {
        // This needs adjusting for dynamic formats
        // if (Screen.width != screen_size.x || Screen.height != screen_size.y) { format = -1; Format(); screen_size = new Vector2(Screen.width, Screen.height); }
        time += Time.deltaTime;
        if (launch_timer > 0) {
            launch_timer += Time.deltaTime;
            print(launch_timer);
            if (launch_timer > 4.5f) { SetButtons(_launched_menu); launch_timer = 0; structure.gameObject.SetActive(true); structure.Launch(); return; }
        }
        if (about_timer < 30) {
            about_timer += Time.deltaTime * 1f;// * GameObject.Find("Slider").GetComponent<Slider>().value;
            // if (about_timer > 073) { if (launched_flag == false) {  launched_flag = true; structure.transform.gameObject.SetActive(true); enemy.SetActive(true); enemy2.SetActive(true);  structure.Launch(); launching = true; structure.Launched = true; } map_camera.orthographicSize = Mathf.Clamp(100 - Mathf.Pow(Mathf.Round((float)(about_timer-073) * 15f) / 15f , 1.2f), 8, 100);  map.GetComponent<AbstractMap>().UpdateMap(15); }
            // else if (about_timer > 067) { map.GetComponent<AbstractMap>().UpdateMap(Mathf.Clamp(Mathf.Pow(Mathf.Round((float)(about_timer-065) * 15f) / 15f, 1.3f), 3f, 15.1f)); }
            About(Math.Round(about_timer));
        }
        if (launching) {
            map_camera.transform.position = structure.transform.position + new Vector3(0, 200, 0);
            // map.GetComponent<AbstractMap>().UpdateMap(structure.Pos);
            // structure.transform.localPosition = map.GetComponent<AbstractMap>().GeoToWorldPosition(structure.Pos, true) + new Vector3(0, 10, 0);
        }
    }
    string selected = "";
    public void OnSelect(string component, string id) {
        print(component);
        print(id);
        if (component == "") { view.text = ""; if (left_title.text==_syntax) { Design(); } return; }
        if (component == "Processor") {
            selected = id;
            processor = structure.GetProcessorController(id);
            Syntax();
            return;
        }
    }
    
    public void Button(int index, bool state) {
        if (!state) ButtonStyle(index, FontStyle.Normal);
        else ButtonStyle(index, FontStyle.Bold);
        buttons[index].transform.GetChild(0).gameObject.SetActive(state);
    }
    public void ButtonStyle(int index, FontStyle style) { 
         buttons[index].transform.GetChild(1).GetComponent<Text>().fontStyle = style;
    } 
    public void EnableGameObject(string name, bool state) {
        GameObject.Find(name).GetComponent<SpriteRenderer>().enabled = state;
    }
    public void Debug(string text){
        debug.text = text;
    }
//⨻⨶✇⒳ⓧ⚠
// E2E Flow via Azure Function App
// StartCoroutine( 
//     GetRequest( // See "https://github.com/bitnaughts/bitnaughts.db/raw/master/example.txt"
//         "https://bitnaughts.azurewebsites.net/api/Mainframe" +
//         "?code=" + code +
//         "&name=s"
//     )
// );processor = structure.GetProcessorController(button_text);  
    public void About(double time) { 
        switch (time) { 
            case 00: view.text = "\n> <b>☄ BitNaughts</b> is\n    an educational\n    programming\n    video-game;";  break;
            case 01: view.text = "\n> ☄ BitNaughts is\n    an <b>educational</b>\n    programming\n    video-game;"; break;
            case 02: view.text = "\n> ☄ BitNaughts is\n    an educational\n    <b>programming</b>\n    video-game;";  break;
            case 03: view.text = "\n> ☄ BitNaughts is\n    an educational\n    programming\n    <b>video-game</b>:\n\n> ▦ Design and\n  ◬ Code artificially\n    intelligent (A.I.)\n  △ Machines;"; break;
            case 04: view.text = "\n> ☄ BitNaughts is\n    an educational\n    programming\n    video-game:\n\n> <b>▦ Design</b> and\n  ◬ Code artificially\n    intelligent (A.I.)\n  △ Machines;"; ButtonStyle(4, FontStyle.Bold); break;
            case 05: view.text = "\n> ☄ BitNaughts is\n    an educational\n    programming\n    video-game:\n\n> ▦ Design and\n  <b>◬ Code</b> artificially\n    intelligent (A.I.)\n  △ Machines;"; ButtonStyle(5, FontStyle.Bold); Button(4, false); break;
            case 06: view.text = "\n> ☄ BitNaughts is\n    an educational\n    programming\n    video-game:\n\n> ▦ Design and\n  ◬ Code <b>artificially</b>\n    intelligent (<b>A.</b>I.)\n  △ Machines;"; Button(5, false); break; 
            case 07: view.text = "\n> ☄ BitNaughts is\n    an educational\n    programming\n    video-game:\n\n> ▦ Design and\n  ◬ Code artificially\n    <b>intelligent</b> (A.<b>I.</b>)\n  △ Machines;"; Button(0, false); break; 
            case 08: view.text = "\n> ☄ BitNaughts is\n    an educational\n    programming\n    video-game:\n\n> ▦ Design and\n  ◬ Code artificially\n    intelligent (<b>A.I.</b>)\n  <b>△ Machines</b>:\n\n  " + _load + " an\n    Example;";  ButtonStyle(2, FontStyle.Bold); break;
            case 09: view.text = "\n> ☄ BitNaughts is\n    an educational\n    programming\n    video-game:\n\n> ▦ Design and\n  ◬ Code artificially\n    intelligent (A.I.)\n  △ Machines:\n\n  <b>" + _load + "</b> an\n    Example;"; Button(2, true); once = false; break;
            case 10: view.text = "\n> ☄ BitNaughts is\n    an educational\n    programming\n    video-game:\n\n> ▦ Design and\n  ◬ Code artificially\n    intelligent (A.I.)\n  △ Machines:\n\n  <b><i>" + _load + "</i></b> an\n   <b> Example</b>;"; Button(2, false); if (once == false) { once = true; StartCoroutine(GetRequest("https://bitnaughts.azurewebsites.net/api/Mainframe?code=" + code + "&name=s")); } break;
            case 11: view.text = "\n> ☄ BitNaughts is\n    an educational\n    programming\n    video-game:\n\n> ▦ Design and\n  ◬ Code artificially\n    intelligent (A.I.)\n  △ Machines:\n\n  " + _load + " an\n    Example;"; break;
            case 12: view.text = "\n> "; break;
            case 13: view.text = "\n> <b>△ Machines</b> are\n    designed with\n  ▢ Objects;"; break;   
            case 14: view.text = "\n> △ Machines are\n    <b>designed</b> with\n  ▢ Objects;"; break;   
            case 15: view.text = "\n> △ Machines are\n    designed with\n  <b>▢ Objects</b>;"; ButtonStyle(4, FontStyle.Bold); break;
            case 16: view.text = "\n> △ Machines are\n    designed with\n  <b><i>▢ Objects</i></b>;"; Button(4, true); break; 
            case 17: view.text = "\n> △ Machines are\n    designed with\n  ▢ Objects;"; Button(4, false); break; 
            case 18: view.text = "\n> "; break;
            case 19: view.text = "\n> <b>△ Machines</b> are\n    programmed with\n  ◬ Syntax;"; break;
            case 20: view.text = "\n> △ Machines are\n    <b>programmed</b> with\n  ◬ Syntax;"; break;
            case 21: view.text = "\n> △ Machines are\n    programmed with\n  <b>◬ Syntax</b>;"; ButtonStyle(5, FontStyle.Bold); break;
            case 22: view.text = "\n> △ Machines are\n    programmed with\n  <b><i>◬ Syntax</i></b>;"; Button(5, true); break;
            case 23: view.text = "\n> △ Machines are\n    programmed with\n  ◬ Syntax;"; Button(5, false); break;
            case 24: view.text = "\n> "; break;
            case 25: view.text = "\n> <b>" + _launch    + "</b> to see\n    it in action!\n"; ButtonStyle(9, FontStyle.Bold); break; 
            case 26: view.text = "\n> <b><i>" + _launch    + "</i></b> to see\n    it in action!\n"; Button(9, true); break; 
            case 27: view.text = "\n> " + _launch    + " to see\n    it in action!\n"; Button(9, false); break;                                           
            case 28: view.text = "\n> "; break;
        }
    }

    public void Format() {
        format++;
        format %= camera_formats.Length;
        if (Screen.width < Screen.height) { if (format < 2) format = 2; }
        else if (format > 1) format = 0;
        map_camera.rect = camera_formats[format];
        var transform = canvas.GetComponent<RectTransform>();
        transform.anchorMin = new Vector2(canvas_formats[format].x, canvas_formats[format].y);
        transform.anchorMax = new Vector2(canvas_formats[format].width, canvas_formats[format].height);
        transform.offsetMin = new Vector2(canvas_padding[format].x, canvas_padding[format].height);
        transform.offsetMax = new Vector2(-canvas_padding[format].width, -canvas_padding[format].y);
    }
    public void SelectOperand() {
        number_operands++;
        switch(operation_button) {
            case _set:
                if (number_operands == 3) Syntax(); return;
                return;
            default:
                if (number_operands < 3) { SetButtons(_operand_menu); return; }
                Syntax(); 
                return;
        }
    }
    bool registering = false, saving = false, loading = false;
    string ship_identifier = ""; 
    public void OnButton(int id)
    {
        if (id == 10) {
            if (!input.gameObject.activeSelf || !input.interactable) return;
            input.gameObject.SetActive(false);
            switch (left_title.text)
            {
                case _launch:
                    return;
            }
            if (registering) { author = input.text; registering = false; Design(); view.text += "\n\n> <i>" + author + "</i>\n  signed in;"; return; }
            if (loading) { Design(); view.text += "\n\n> <i>" + input.text + "</i>\n  loading...";      if (input.text != "") { StartCoroutine(              GetRequest(                   "https://bitnaughts.azurewebsites.net/api/Mainframe" +                  "?code=" + code +                  "&name=" + input.text              )          );          loading = false;      }      return;  } // See "https://github.com/bitnaughts/bitnaughts.db/raw/master/" + input.text + ".txt"
            if (saving) { view.text += "\n\n> <i>" + input.text + "</i>\n  saving...";  if (input.text != "") { StartCoroutine(          GetRequest(              "https://bitnaughts.azurewebsites.net/api/Mainframe" +              "?code=" + code +              "&author=" + author +              "&name=" + input.text +               "&data=" + structure.ToString()          )      );  }  return; } return;
        }
        string button_text = buttons[id].transform.GetChild(1).GetComponent<Text>().text;
        print(button_text);
     
        switch (button_text) {
            case _zoom_in: if (map_camera.orthographicSize > 4) map_camera.orthographicSize/=2; return;
            case _zoom_out: if (map_camera.orthographicSize < 128) map_camera.orthographicSize*=2; return;
        }
     
        switch (left_title.text)
        {
            case _editor:
                buttons[id].transform.GetChild(0).gameObject.SetActive(true);
                switch (button_text) {
                    case _about: about_timer = 0; return;
                    case _campaign: left_title.text = _campaign; SetButtons(_missions_menu); view.text = "\n> <b>" + _campaign + "</b> teach\n    game mechanics\n    and computer\n    science principles;"; return;
                    case _objects: left_title.text = _objects; SetButtons(_objects_menu);  return;
                    case _syntax:  
                        var processors = structure.GetProcessorControllers();
                        switch (processors.Length) {
                            case 0: buttons[id].transform.GetChild(0).gameObject.SetActive(false); view.text = "\n  ▥ Processor needed;"; return;
                            case 1: processor = structure.GetProcessorController(processors[0]); plotter.Select(processors[0]); Syntax(); return;
                            default: left_title.text = _processor; SetButtons(structure.GetProcessorControllers()); return;
                        }
                        // return;
                    case _launch:  LaunchMenu(); map_camera.orthographicSize = 64; structure.gameObject.SetActive(false); return;
                    case _new:     left_title.text = _objects; SetButtons(_objects_menu); return; //view.text += author == default_author ? "\n\n> <b>Register</b>\n  your GitHub!" : "\n\n> <i>" + author + "</i>\n  signed in;"; 
                    case _save: if (saving) { Design(); return; } saving = true; view.text = "\n> <b>≛ Database</b>"; input.m_DialogTitle = "> Input a ship identifier;"; placeholder.text = "≙"; input.gameObject.SetActive(true); return;
                    case _load: if (loading) { Design(); return; } loading = true; view.text = "\n> <b>≛ Database</b>"; input.m_DialogTitle = "> Input a ship identifier;"; input.text = ""; placeholder.text = "≚"; input.gameObject.SetActive(true); return;
                    case _settings: left_title.text = _settings; SetButtons(settings_options); return;
                } return;
            case _campaign:
                if (id == 9) { Design(); return; }
                switch (button_text) {
                    // case _logs:         view.text = "\n> <b>" + _logs + "</b> enable\n    monitoring of\n  △ Machines;\n\n  Challenge:\n    Build a\n  △ Machine with\n    at least\n    one of every\n  "+_objects+"!\n\nLesson:\n    Double-click\n    coming soon...";     return;
                    // case _metrics:      view.text = "\n> <b>" + _metrics + "</b> enable\n    monitoring of\n  △ Machines;\n\n  Challenge:\n    Build a\n  △ Machine with\n    at least\n    one of every\n  "+_objects+"!\n\nLesson:\n    Double-click\n    coming soon...";         return;
                    // case _syntax:       view.text = "\n> <b>" + _syntax + "</b> is\n    how we arrange\n    symbols into expressions;\n\n  Challenge:\n    Build a\n  △ Machine with\n    at least\n    one of every\n  "+_objects+"!\n\nLesson:\n    Double-click\n    coming soon...";    return;
                    case _objects:      view.text = "\n> <b>" + _objects + "</b> are\n    building blocks of\n  △ Machines;\n\n  Challenge:\n    Build a\n  △ Machine with all\n  "+_objects+"!\n\nLesson:\n    Double-click\n    coming soon..."; return;
                    case _objects2:     view.text = "\n> <b>" + _objects2 + "s</b> enable\n  " + _syntax +"\n    to control\n  " + _objects + ";\n\n  Challenge:\n    Build a\n  △ Machine that\n    controls all\n  "+_objects+"!\n\nLesson:\n    Double-click\n    coming soon...";   return;
                    case _boolean:      view.text = "\n> <b>" + _boolean + "s</b> enable\n    discrete mathematics;\n\n  Challenge:\n    Build a\n  △ Machine that computes every boolean function!\n\nLesson:\n    Double-click\n    coming soon...";   return;
                    case _arithmetic:   view.text = "\n> <b>" + _arithmetic + "</b> enables\n    analog computation;\n\n  Challenge:\n    Build a\n  △ Machine that computes every boolean function!\n\nLesson:\n    Double-click\n    coming soon...";    return;
                    case _flow_control: view.text = "\n> <b>" + _flow_control + "</b> controls\n    code execution;\n\n  Challenge:\n    Build a\n  △ Machine that computes every boolean function!\n\nLesson:\n    Double-click\n    coming soon...";    return;
                    case _trigonometry: view.text = "\n> <b>" + _trigonometry + "</b> enables\n    circular mathematics;\n\n  Challenge:\n    Build a\n  △ Machine that computes every boolean function!\n\nLesson:\n    Double-click\n    coming soon...";    return;
                }
                return;
            case _launch:
                if (id == 8) { left_title.text = _settings; SetButtons(settings_options); return; }
                if (id == 9) { Design(); map_zoom = 2; return; }
                for (int i = 0; i < _launch_menu.Length; i++) if (button_text == _launch_menu[i]) { SetButtons(continent_cities[i]); 
                 left_title.text = button_text;} return;
            case _monitor:
                if (id == 9) { LaunchMenu(); map_zoom = 2; launching = false; return; }
                switch (button_text) {
                    case _settings: left_title.text = _settings; SetButtons(settings_options); return;
                    case _objects: SetButtons(structure.GetControllers());  return;
                    case _syntax: SetButtons(structure.GetProcessorControllers());  return;
                    case _metrics: SetButtons(structure.GetControllers());  return;
                    case _logs: SetButtons(structure.GetProcessorControllers());  return;
                } return;
            case _settings:
                switch (button_text) {
                    case _audio_up: audio.volume += .1f; view.text = "\n> <b>Audio</b>: " + audio.volume; return;
                    case _audio_down: audio.volume -= .1f; view.text = "\n> <b>Audio</b>: " + audio.volume; return;
                    case _button_size_up: button_size += .1f; for (int i = 0; i < buttons.Length; i++) buttons[i].GetComponent<RectTransform>().anchorMax = new Vector2(button_size, buttons[i].GetComponent<RectTransform>().anchorMax.y); return;
                    case _button_size_down: button_size -= .1f; for (int i = 0; i < buttons.Length; i++) buttons[i].GetComponent<RectTransform>().anchorMax = new Vector2(button_size, buttons[i].GetComponent<RectTransform>().anchorMax.y); return;
                    case _flip: flip = !flip; return;
                        // if (flip) {
                        //     for (int i = 0; i < buttons.Length; i++)
                        //         // buttons[i].GetComponent<RectTransform>().anchorMin = new Vector2(0, buttons[i].GetComponent<RectTransform>().anchorMin.y);
                        //         buttons[i].GetComponent<RectTransform>().anchorMax = new Vector2(button_size, buttons[i].GetComponent<RectTransform>().anchorMax.y);
                        // else {
                        //     for (int i = 0; i < buttons.Length; i++)
                        //         buttons[i].GetComponent<RectTransform>().anchorMin = new Vector2(1-button_size, buttons[i].GetComponent<RectTransform>().anchorMin.y);
                        //         buttons[i].GetComponent<RectTransform>().anchorMax = new Vector2(1, buttons[i].GetComponent<RectTransform>().anchorMax.y);
                        //         // buttons[i].GetComponent<RectTransform>().anchorMin = new Vector2(1 - button_size, buttons[i].GetComponent<RectTransform>().anchorMin.y);
                        //         // buttons[i].GetComponent<RectTransform>().anchorMax = new Vector2(buttons[i].GetComponent<RectTransform>().anchorMax.x, buttons[i].GetComponent<RectTransform>().anchorMax.y);
                    case _reorient: Format(); return;
                    case _font_size_up: if (view.fontSize < 35) { for (int i = 0; i < buttons.Length; i++) buttons[i].transform.GetChild(1).GetComponent<Text>().fontSize++; left_title.fontSize++; right_title.fontSize++; view.fontSize++; view.text = "\n> <b>Font Size</b>: " + view.fontSize; } return;
                    case _font_size_down: if (view.fontSize > 5) {  for (int i = 0; i < buttons.Length; i++) buttons[i].transform.GetChild(1).GetComponent<Text>().fontSize--; left_title.fontSize--; right_title.fontSize--; view.fontSize--; view.text = "\n> <b>Font Size</b>: " + view.fontSize; } return;
                    case _debug_mode:     return;
                    case _back: Design(); return;
                } return;
            case _objects:
                if (id == 9) { Design(); return; }
                SetButtons(_objects_menu); buttons[id].transform.GetChild(0).gameObject.SetActive(true);
                switch (button_text) {
                    case _processor: view.text = "\n  " +  _processor + "\n  " + _processor_description; return;
                    case _cache:     view.text = "\n  " + _cache      + "\n  " + _cache_description;     return;
                    case _girder:    view.text = "\n  " + _girder     + "\n  " + _girder_description;    return;
                    case _bulkhead:  view.text = "\n  " + _bulkhead   + "\n  " + _bulkhead_description;  return;
                    case _gimbal:    view.text = "\n  " + _gimbal     + "\n  " + _gimbal_description;    return;
                    case _thruster:  view.text = "\n  " + _thruster   + "\n  " + _thruster_description;  return;
                    case _booster:   view.text = "\n  " + _booster    + "\n  " + _booster_description;   return;
                    case _sensor:    view.text = "\n  " + _sensor     + "\n  " + _sensor_description;    return;
                    case _cannon:    view.text = "\n  " + _cannon     + "\n  " + _cannon_description;    return;
                } return;
            case _processor:
                left_title.text = _syntax; SetButtons(_syntax_menu);
                structure.GetComponentToString(button_text);
                plotter.Select(button_text);
                selected = button_text;
                processor = structure.GetProcessorController(button_text);
                return;
            case _syntax: // Edit Code
                if (id == 9) { Design(); plotter.Deselect(); return; }
                switch (button_text) {
                    case _scroll_up:     processor.Scroll(-1);   return; // Navigation
                    case _add_above:     processor.AddLine(1);   return;
                    case _delete:        processor.DeleteLine(); return;
                    case _add_below:     processor.AddLine(1);   return;
                    case _scroll_down:   processor.Scroll(1);    return;
                    case _arithmetic:    left_title.text = _arithmetic;   SetButtons(_arthimetic_menu);   return; // Operation Categories
                    case _flow_control:  left_title.text = _flow_control; SetButtons(_flow_control_menu); return;
                    case _boolean:       left_title.text = _boolean;      SetButtons(_boolean_menu);      return;
                    case _trigonometry:  left_title.text = _trigonometry; SetButtons(_trigonometry_menu); return;
                    case _objects2:      if (structure.GetInteractiveComponents().Length == 0) { buttons[id].transform.GetChild(0).gameObject.SetActive(false); view.text = "\n  ▢ Objects needed;"; return; } left_title.text = _objects2; number_operands++; processor.SetOperand("obj"); SetButtons(structure.GetInteractiveComponents()); return;
                } return;
            case _arithmetic:
            case _flow_control:
            case _boolean:
            case _trigonometry:
            case _objects2:
                switch (button_text) {
                    case _back:    Syntax();                              return;
                    case _enter:   SelectOperand();                       return;
                    // case _add: processor.SetOperand("add"); return;
                    // case _subtract: processor.SetOperand("sub"); return;
                    // case _absolute: processor.SetOperand("abs"); return;
                    // case _multiply: processor.SetOperand("mul"); return;
                    // case _divide: processor.SetOperand("div"); return;
                    // case _modulo: processor.SetOperand("mod"); return;
                    // case _exponential: processor.SetOperand("exp"); return;
                    // case _root: processor.SetOperand("roo"); return;


                    case _jump:   processor.SetOperand("jum"); SetButtons(processor.GetLabels()); return;
                    case _jumpLabel: input.gameObject.SetActive(true); input.text = ""; placeholder.text = _jumpLabel; SetButtons(_jump_label_menu); return;
                    case "+ 100": processor.ModifyConstantOperand(100);   return;
                    case "- 100": processor.ModifyConstantOperand(-100);  return;
                    case "+ 10.":  processor.ModifyConstantOperand(10);   return;         
                    case "- 10.":  processor.ModifyConstantOperand(-10);  return;         
                    case "+ 1.0": processor.ModifyConstantOperand(1);     return;
                    case "- 1.0": processor.ModifyConstantOperand(-1);    return;
                    case "+ 0.1": processor.ModifyConstantOperand(.1f);   return;
                    case "- 0.1": processor.ModifyConstantOperand(-.1f);  return;
                    case _variable: SetButtons(processor.GetVariables()); return;
                    case _random: processor.AddOperand(" rnd"); Syntax(); return;
                    case _constant: processor.AddOperand(" 0"); SetButtons(_constant_menu); return;
                    case _set: operation_button = _set;  number_operands += 2; processor.SetOperand("set"); input.gameObject.SetActive(true); input.text = "a"; placeholder.text = _variable; SetButtons(_variable_menu); return;
                    case _ok: processor.AddOperand(" " + input.text); input.gameObject.SetActive(false); SetButtons(_operand_menu); return;
                    default: if (structure.IsComponent(button_text)) { processor.AddOperand(" " + button_text); SelectOperand(); return; } if (processor.IsVariable(button_text)) { processor.AddOperand(" " + button_text); SelectOperand(); return; } if (processor.IsLabel(button_text)) { processor.AddOperand(" " + button_text); Syntax(); return; }
                             number_operands++; processor.SetOperand(Interpreter.GetTextCode(button_text)); SetButtons(processor.GetVariables()); return;
                }
            case _metrics:  if (id == 9) { Design(); return; } return;
        }
    }
    public void LaunchMenu() {
        form.sprite = form_launch;
        arrow.sprite = arrow_exit;
        left_title.text = _launch;
        right_title.text = _map;
        map_camera.gameObject.SetActive(true);
        map_camera.backgroundColor = new Color(33/255f, 34/255f, 39/255f);
        map.SetActive(true);                        
        // input.m_DialogTitle = "Enter a location:";
        // input.text = "";
        placeholder.text = "✵";
        // input.gameObject.SetActive(true);
        EnableGameObject("7", false);
        EnableGameObject("37", false);
        plotter.GetComponent<SpriteRenderer>().enabled = false;
        SetButtons(_launch_menu);
    }

    public void OnClick() {
        if (left_title.text == _menu) {
            Design();
            return;
        }
        launched = !launched;
        if (launched) LaunchMenu();
        else          Design();
    }

    GameObject MakeObject(GameObject prefab, string name, string location, string size, string rotation) {
        var object_instance = Instantiate(prefab, new Vector3(float.Parse(location.Split(',')[0]), 0,  float.Parse(location.Split(',')[1])), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        object_instance.name = name;
        object_instance.GetComponent<SpriteRenderer>().size = new Vector2(float.Parse(size.Split(',')[0]), float.Parse(size.Split(',')[1]));
        object_instance.transform.SetParent(structure.transform.Find("Rotator"));
        object_instance.transform.localPosition = new Vector2(
            Mathf.Round(object_instance.transform.localPosition.x),
            Mathf.Round(object_instance.transform.localPosition.y)
        );
        object_instance.transform.localEulerAngles = new Vector3(0, 0, float.Parse(rotation));
        return object_instance;
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return webRequest.SendWebRequest();
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    view.text += "\n\n< " + webRequest.error.ToString();
                    break;
                case UnityWebRequest.Result.Success:
                    var lines = webRequest.downloadHandler.text.ToString().Split('\n');
                    for (int i = 0; i < lines.Length; i++) {
                        print(i + " " + lines[i]);
                        if (lines[i].Contains("▥")) {
                            var processor = MakeObject(_Processor,
                                lines[i].Split('▥')[1].Trim().Replace("<b>", "").Replace("</b>", ""),
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('┗')[1].Split('°')[0].Trim());
                            i += 2;
                            List<string> instructions = new List<string>();
                            while (lines[i] != "") {
                                var line = lines[i].Substring(3).Replace("<b>", "").Replace("</b>", ""); 
                                instructions.Add(line);
                                ++i;
                            }
                            processor.GetComponent<ProcessorController>().SetInstructions(instructions);
                        }
                        if (lines[i].Contains("▤")) {
                            MakeObject(_Cache,
                                lines[i].Split('▤')[1].Trim().Replace("<b>", "").Replace("</b>", ""),
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('┗')[1].Split('°')[0].Trim());
                        }
                        if (lines[i].Contains("▨")) {
                            MakeObject(_Girder,
                                lines[i].Split('▨')[1].Trim().Replace("<b>", "").Replace("</b>", ""),
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('┗')[1].Split('°')[0].Trim());
                        }
                        if (lines[i].Contains("▣")) {
                            MakeObject(_Gimbal,
                                lines[i].Split('▣')[1].Trim().Replace("<b>", "").Replace("</b>", ""),
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('┗')[1].Split('°')[0].Trim());
                        }
                        if (lines[i].Contains("▩")) {
                            MakeObject(_Bulkhead,
                                lines[i].Split('▩')[1].Trim().Replace("<b>", "").Replace("</b>", ""),
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('┗')[1].Split('°')[0].Trim());
                        }
                        if (lines[i].Contains("◍")) {
                            MakeObject(_Cannon,
                                lines[i].Split('◍')[1].Trim().Replace("<b>", "").Replace("</b>", ""),
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('┗')[1].Split('°')[0].Trim());
                        }
                        if (lines[i].Contains("◉")) {
                            MakeObject(_Thruster,
                                lines[i].Split('◉')[1].Trim().Replace("<b>", "").Replace("</b>", ""),
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('┗')[1].Split('°')[0].Trim());
                        }
                        if (lines[i].Contains("◎")) {
                            MakeObject(_Booster,
                                lines[i].Split('◎')[1].Trim().Replace("<b>", "").Replace("</b>", ""),
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('┗')[1].Split('°')[0].Trim());
                        }
                        if (lines[i].Contains("◌")) {
                            MakeObject(_Sensor,
                                lines[i].Split('◌')[1].Trim().Replace("<b>", "").Replace("</b>", ""),
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('(')[1].Split(')')[0],
                                lines[++i].Split('┗')[1].Split('°')[0].Trim());
                        }
                    }
                    break;
            }
        }
    }
}
