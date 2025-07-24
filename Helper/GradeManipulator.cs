namespace SubjectHelper.Helper;

public static class GradeManipulator
{
    public static string GetGrade(int grade)
    {
        return grade switch
        {
            >= 95 => "A",
            >= 85 => "A-",
            >= 80 => "B+",
            >= 75 => "B",
            >= 70 => "B-",
            >= 65 => "C+",
            >= 55 => "C",
            >= 45 => "E",
            _ => "F"
        };
    }

    public static int GetGrade(string grade)
    {
        return grade switch
        {
            "A" => 95,
            "A-" => 85,
            "B+" => 80,
            "B" => 75,
            "B-" => 70,
            "C+" => 65,
            "C" => 55,
            "E" => 45,
            _ => 0
        };
    }
}