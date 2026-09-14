using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;


public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

	public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;
	}

	protected async override void OnAppearing()
	{
		try
		{
			lista.Clear();

			List<Produto> tmp = await App.Db.GetAll();

			tmp.ForEach(i => lista.Add(i));
		}
		catch ( Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "Ok");
		}

	}

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{

			Navigation.PushAsync(new Views.NovoProduto());

		}catch (Exception ex)
		{

			DisplayAlert("ops", ex.Message, "ok");
 		}
    }

	private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
	{
		try
		{
			

			string q = e.NewTextValue;
            lst_produtos.IsRefreshing = true;
            /* eu me perguntei o que esse clear faz, e descobrir, tiramdo ela do codigo, que ela, alem de
			ser responsavel por mostrar o item pesquisado, enquanto "oculta" os outros itens, sem ela, os itens
			nao pesquisados nao sao ocultados, inutilizando essa função, alem disso ele começa a apresentar
			o mesmo comportamento, de duplicar os itens da lista, de forma indefinida, parecida com o bug 
			visto quando se adiciona itens na lista.*/
            lista.Clear();

			List<Produto> tmp = await App.Db.search(q);

			tmp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "oK");		
		}
		finally
		{
            lst_produtos.IsRefreshing = false;
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {

		double soma = lista.Sum(i => i.Total);

		string msg = $"O Total é {soma:C}";

		DisplayAlert("TOtal dos Produtos", msg, "OK");

    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
		/*bom eu tentei fazer o botao: tentei aplicar a seguinte logica;
			criar uma variavel para referenciar o Id, depois apagar uzando o Delete la no SqLite
		
		 Resultado: nao consegui :(.
		apos isso eu resolvi verificar o gabarito na aula da ag5*/

		try
		{
			MenuItem Selecionado = sender as MenuItem;

			Produto p = Selecionado.BindingContext as Produto;

			bool comfirm = await DisplayAlert("Tem certeza ?", $"remover {p.Descricao}?", "sim", "Não");
			if(comfirm)
			{
				await App.Db.delete(p.Id);
				lista.Remove(p);
			}
		}
		catch (Exception ex)
		{
			DisplayAlert("ops", ex.Message, "ok");
		}



    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

		try
		{
			Produto p = e.SelectedItem as Produto;
			Navigation.PushAsync(new Views.EditarProduto
			{
				BindingContext = p,
			});
		}
        catch (Exception ex)
        {
            DisplayAlert("ops", ex.Message, "ok");
        }
    }

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
		finally
		{
			lst_produtos.IsRefreshing = false;	
			
		}
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            DateTime datainicio = dp_inicio.Date;
            DateTime datafim = dp_fim.Date;

            lst_produtos.IsRefreshing = true;
            lista.Clear();
            List<Produto> tmp = await App.Db.periodo(datainicio, datafim);

			
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "oK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }
}