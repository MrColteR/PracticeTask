using PracticeTask.Model;
using PracticeTask.Model.Base;
using PracticeTask.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.Factory
{
    public interface ICircleInteraction
    {
        ObservableCollection<Circle> CreateElipse(); // Создание шариков 
        void Timer_Tick(ObservableCollection<Circle> circles,ref int IsStop, double heightItemsControl, double widthItemsControl); // Перемещение шариков на экране
        ObservableCollection<Circle> Timer_Restart(ObservableCollection<Circle> circles, bool IsRight); //Добавление шарика при правильном ответе
    }
}
