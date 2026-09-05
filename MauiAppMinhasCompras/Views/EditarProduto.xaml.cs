using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto Produto_anexado = BindingContext as Produto;

            Produto p = new Produto
            {
                Id = Produto_anexado.Id,
                Descricao = txt_descrição.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            await App.Db.Update(p);
            await DisplayAlert("sucesso!", "Registro atualizado", "ok");
            await Navigation.PopAsync();

        }
        catch (Exception ex)
        {
            DisplayAlert("ops", ex.Message, "OK");
        }
    }
}