using System;
using System.Collections.Generic;
using Android.Views;
using Android.Animation;

namespace Moyeu
{
	public static class AnimationExtensions
	{
		static Dictionary<View, Animator> currentAnimations = new Dictionary<View, Animator> ();

		public static void AlphaAnimate (this View view, float alpha, int duration = 300, Action endAction = null, int startDelay = 0)
		{
			Animator oldAnimator;
			if (currentAnimations.TryGetValue (view, out oldAnimator))
				oldAnimator.Cancel ();
			var animator = ObjectAnimator.OfFloat (view, "alpha", view.Alpha, alpha);
			currentAnimations [view] = animator;
			animator.SetDuration (duration);
			animator.StartDelay = startDelay;
			animator.AnimationEnd += (sender, e) => {
				currentAnimations.Remove (view);
				if (endAction != null)
					endAction ();
			};
			animator.Start ();
		}
	}
}

