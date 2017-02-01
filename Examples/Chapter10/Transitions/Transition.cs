﻿using Boc.Domain.Events;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using System;

namespace Boc.Chapter10
{
   public delegate Validation<ValueTuple<T, St>> Transition<St, T>(St state);

   public static class Transition
   {
      public static Transition<St, R> Select<St, T, R>
         (this Transition<St, T> transition, Func<T, R> project)
         => state0 
         => transition(state0)
            .Map(result => Tuple(project(result.Item1), result.Item2));

      public static Transition<St, R> SelectMany<St, T, R>
         (this Transition<St, T> transition, Func<T, Transition<St, R>> f)
         => state0 
         => transition(state0)
            .Bind(t => f(t.Item1)(t.Item2));

         //   var(t, state1) = StatefulComputation(state0);
         //   return f(t)(state1);
         //};

      public static Transition<St, RR> SelectMany<St, T, R, RR>
         (this Transition<St, T> transition
         , Func<T, Transition<St, R>> bind
         , Func<T, R, RR> project)
         => state0
         => transition(state0)
            .Bind(t => bind(t.Item1)(t.Item2)
               .Map(r => Tuple(project(t.Item1, r.Item1), r.Item2)));
         
      //   var(r, state2) = bind(t)(state1);
         //   var rr = project(t, r);
         //   return (rr, state2);
         //};
   }
}
