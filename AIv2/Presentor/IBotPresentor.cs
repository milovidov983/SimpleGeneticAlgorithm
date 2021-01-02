namespace AiLib.Presentor {
	public interface IBotPresentor {
		BotView[] GetViews();
		void SetNextStep();
	}
}
