using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconMigrationTool.Experiments
{
    public abstract class GenericTest
    {
    }

    public abstract class Day
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class Holyday: Day
    {
        public virtual string AdditionalName { get; set; }
    }

    public class Workday: Day
    {
        public virtual int HoursToWork { get; set; }
    }

    public abstract class DaySchedule<T> where T: Day
    {
        public virtual int Id { get; set; }
        public virtual T Day { get; set; }
    }

    public class HolydaySchedule: DaySchedule<Holyday>
    {
        public virtual string SomeString { get; set; }
    }

    public class WorkdaySchedule: DaySchedule<Workday>
    {
        public virtual string SomethingElseString { get; set; }
    }
}
