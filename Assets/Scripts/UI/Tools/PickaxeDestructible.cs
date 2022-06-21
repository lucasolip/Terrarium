public class PickaxeDestructible : MouseHandler
{
    bool clicked;

    public override void Clicked()
    {
        clicked = true;
    }

    public override void Dragged()
    {
    }

    public override void Released()
    {
        if (MouseController.selectedTool.tool == Tool.Pickaxe && clicked)
        {
            BarrenBlock barrenBlock = GetComponent<BarrenBlock>();
            if (barrenBlock != null) {
                barrenBlock.terrain.FertilizeBlock(barrenBlock);
                Destroy(barrenBlock);
                Destroy(this);
            }
        }
        clicked = false;
    }
}