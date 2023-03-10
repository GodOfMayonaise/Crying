using UnityEngine;
using UnityEngine.UI;

public class IndicationBar : MonoBehaviour
{
    public Color fillColor = Color.red;
    public Color fadeColor = Color.grey;
    public Color backgroundColor = Color.black;

    [SerializeField] private Image fill;
    [SerializeField] private Image fade;
    [SerializeField] private Image background;
    [SerializeField] private Slider fillSlider;
    [SerializeField] private Slider fadeSlider;

    public float maxValue;
    public float value;
    public float fadeDelay = 3f;
    public float fadeSpeed = 1f;
    private float fadeTimer;

    public void Awake() {
        initSliders();
        setValue(50);
    }

    public void init(Color fillColor, Color fadeColor, Color backgroundColor, float maxValue, float value) {
        this.fillColor = fillColor;
        this.fadeColor = fadeColor;
        this.backgroundColor = backgroundColor;
        this.maxValue = maxValue;
        this.value = value;
        initSliders();
    }

    public void initSliders() {
        fillSlider.maxValue = maxValue;
        fadeSlider.maxValue = maxValue;
        
        fillSlider.value = value;
        fadeSlider.value = value;

        fill.color = fillColor;
        fade.color = fadeColor;
        background.color = backgroundColor;
    }

    public void setValue(float value) {
        if(value < this.value) {
            fadeTimer = fadeDelay;
        }

        this.value = value;
        fillSlider.value = value;
    }

    public void setMaxValue(float ammount) {
        fillSlider.maxValue = ammount;
        fadeSlider.maxValue = ammount;
    }

    public void Update() {
        fadeTimer -= Time.deltaTime;
        if(fadeSlider.value <= value) return;
        if(fadeTimer < 0) {
            fadeSlider.value -= Time.deltaTime * fadeSpeed * fadeSlider.maxValue;
            fadeSlider.value = Mathf.Max(fadeSlider.value, value);
        }
    }
}
