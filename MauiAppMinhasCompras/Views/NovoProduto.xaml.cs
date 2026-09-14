using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			TimeSpan horaAgora = DateTime.Now.TimeOfDay;
			DateTime dataColoca = dp_data.Date;
			DateTime dataagora = dataColoca.Add(horaAgora);

			Produto p = new Produto
			{
				Descricao = txt_descrição.Text,
				Quantidade = Convert.ToDouble(txt_quantidade.Text),
				Preco = Convert.ToDouble(txt_preco.Text),
				DataHora= dataagora
            };

			await App.Db.insert(p);
			await DisplayAlert("sucesso!", "Registro Inserido", "ok");
			await Navigation.PopAsync();
		} catch (Exception ex)
		{
			DisplayAlert("ops", ex.Message, "OK");
		}
    }
}