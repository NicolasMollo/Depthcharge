namespace Depthcharge.UI
{
    public struct EndGameMenuTexts
    {
        public string enemiesDefeatedText;
        public string enemiesMissedText;
        public string scoreText;
        public string timeText;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndGameMenuTexts"/> struct with the specified texts.
        /// </summary>
        /// <param name="texts">An array of strings representing the texts to be displayed in the end game menu. Each string corresponds to
        /// a specific menu item or message.</param>
        public EndGameMenuTexts(string[] texts)
        {
            enemiesDefeatedText = texts[0];
            enemiesMissedText = texts[1];
            scoreText = texts[2];
            timeText = texts[3] != null ? texts[3] : string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EndGameMenuTexts"/> struct with the specified text values for
        /// the end-game menu.
        /// </summary>
        /// <param name="enemiesDefeatedText">The text to display for the number of enemies defeated.</param>
        /// <param name="enemiesMissedText">The text to display for the number of enemies missed.</param>
        /// <param name="scoreText">The text to display for the player's score.</param>
        /// <param name="timeText">The text to display for the elapsed time.</param>
        public EndGameMenuTexts(string enemiesDefeatedText, string enemiesMissedText, string scoreText, string timeText = null)
        {
            this.enemiesDefeatedText = enemiesDefeatedText;
            this.enemiesMissedText = enemiesMissedText;
            this.scoreText = scoreText;
            this.timeText = timeText == null ? string.Empty : timeText;
        }

    }
}