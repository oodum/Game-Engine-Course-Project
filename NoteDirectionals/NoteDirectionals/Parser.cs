namespace NotePositionals {
    public static class Parser {
        public static Position[] Parse(float[] durations) {
            float timing = 0;
            Position[] positions = new Position[durations.Length];
            for (int i = 0; i < durations.Length; i++)
            {
                var duration = durations[i];
                var position = duration.IsNegative()
                    ? Position.None
                    : i < durations.Length - 1 && durations[i + 1].IsNegative() && timing.OnBeat()
                        ? Position.None
                        : i > 0 && durations[i - 1].IsNegative() && !timing.OnBeat()
                            ? Position.None
                            : timing.OnBeat()
                                ? Position.Left
                                : Position.Right;
                timing += duration.Absolute();
                positions[i] = position;
            }
            return positions;
        }
        static bool IsNegative(this float value) => value < 0;
        static float Absolute(this float value) => value.IsNegative() ? -value : value;
        static bool OnBeat(this float value) => value % 1 == 0;
    }
    public enum Position {
        Left,
        Right,
        None,
    }
}
