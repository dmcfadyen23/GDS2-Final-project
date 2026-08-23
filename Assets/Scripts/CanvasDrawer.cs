using PDollarGestureRecognizer;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasDrawer : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [Header("Canvas Settings")]
    [SerializeField] private Color canvasBackgroundColor = new Color(174, 175, 147);

    [Header("Brush Settings")]
    [SerializeField] private Color brushColor = Color.red;
    [SerializeField] private int brushRadius = 5;

    private Color[] colours = new Color[]
    {
        Color.red, Color.blue, Color.green
    };

    private int colourIndex = 0;
    public void ToggleBrushColour()
    {
        brushColor = colours[++colourIndex];
        if (colourIndex >= 2) colourIndex = -1;
    }

    private Texture2D drawingTexture;
    private RawImage rawImage;
    private RectTransform rectTransform;
    private Vector2 lastPosition;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
        // 1. Initialize texture and clear background
        drawingTexture = new Texture2D((int)rectTransform.rect.width, (int)rectTransform.rect.height, TextureFormat.RGBA32, false);
        rawImage.texture = drawingTexture;
        ClearCanvas();
    }

    public void ClearCanvas()
    {
        Color[] clearColors = new Color[(int)rectTransform.rect.width * (int)rectTransform.rect.height];
        for (int i = 0; i < clearColors.Length; i++) 
            clearColors[i] = canvasBackgroundColor;
        
        drawingTexture.SetPixels(clearColors);
        drawingTexture.Apply();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Capture initial tap position
        TryDraw(eventData.position, out lastPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Interpolate lines while moving to prevent gaps when dragging fast
        if (TryDraw(eventData.position, out Vector2 currentPixelPos))
        {
            DrawLine(lastPosition, currentPixelPos);
            lastPosition = currentPixelPos;
        }
    }

    private bool TryDraw(Vector2 screenPos, out Vector2 pixelPos)
    {
        pixelPos = Vector2.zero;

        // Convert screen coordinates to local RectTransform coordinates
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPos, null, out Vector2 localPoint))
        {
            // Normalize coordinates from (-width/2, width/2) to (0, 1)
            float normalizedX = (localPoint.x + rectTransform.rect.width * 0.5f) / rectTransform.rect.width;
            float normalizedY = (localPoint.y + rectTransform.rect.height * 0.5f) / rectTransform.rect.height;

            // Map normalized coordinates directly onto pixel indices
            pixelPos.x = Mathf.Clamp(normalizedX * (int)rectTransform.rect.width, 0, (int)rectTransform.rect.width);
            pixelPos.y = Mathf.Clamp(normalizedY * (int)rectTransform.rect.height, 0, (int)rectTransform.rect.height);
            
            return true;
        }
        return false;
    }

    private void DrawLine(Vector2 start, Vector2 end)
    {
        DrawCheck drawCheck = GetComponent<DrawCheck>();
        drawCheck.candidatePoints.Add(new Point(start.x, start.y, 0));
        // Bresenham's Line Algorithm to ensure continuous solid strokes
        int x0 = (int)start.x;
        int y0 = (int)start.y;
        int x1 = (int)end.x;
        int y1 = (int)end.y;

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            DrawBrushCircle(x0, y0);
            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
        }
        drawingTexture.Apply(); // Apply pixel changes to the graphics card
    }

    private void DrawBrushCircle(int cx, int cy)
    {
        // Draw brush strokes within a bounded circular footprint
        for (int y = -brushRadius; y <= brushRadius; y++)
        {
            for (int x = -brushRadius; x <= brushRadius; x++)
            {
                if (x * x + y * y <= brushRadius * brushRadius)
                {
                    int pixelX = cx + x;
                    int pixelY = cy + y;

                    if (pixelX >= 0 && pixelX < (int)rectTransform.rect.width && pixelY >= 0 && pixelY < (int)rectTransform.rect.height)
                    {
                        drawingTexture.SetPixel(pixelX, pixelY, brushColor);
                    }
                }
            }
        }
    }
}