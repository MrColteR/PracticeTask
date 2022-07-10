using PracticeTask.Model;
using PracticeTask.Model.Base;
using PracticeTask.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.Factory
{
    public interface ICircleInteraction
    {
        IEnumerable<Circle> CreateElipse(); // Создание шариков IEnumerable
        void Timer_Tick(IEnumerable<Circle> circles, ref int cycleTime, ref bool IsCompleted, double heightScreen, double widthScreen); // Перемещение шариков на экране
        void Timer_Restart(IEnumerable<Circle> circles, bool IsRight); //Добавление шарика при правильном ответе
    }
}
