using System.Text;

namespace SuperbReads.Application.Common;

public static class Slug
{
    public const int MaxLength = 80;

    public static string Create(bool toLower, params string[] values)
    {
        return Create(toLower, string.Join("-", values));
    }

    /// <summary>
    ///     Creates a slug.
    ///     References:
    ///     http://www.unicode.org/reports/tr15/tr15-34.html
    ///     https://meta.stackexchange.com/questions/7435/non-us-ascii-characters-dropped-from-full-profile-url/7696#7696
    ///     https://stackoverflow.com/questions/25259/how-do-you-include-a-webpage-title-as-part-of-a-webpage-url/25486#25486
    ///     https://stackoverflow.com/questions/3769457/how-can-i-remove-accents-on-a-string
    ///     https://stackoverflow.com/questions/25259/how-does-stack-overflow-generate-its-seo-friendly-urls
    /// </summary>
    /// <param name="toLower"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string Create(bool toLower, string value)
    {
        var normalised = value.Normalize(NormalizationForm.FormKD);

        var length = normalised.Length;
        var prevDash = false;
        var sb = new StringBuilder(length);

        for (var i = 0; i < length; i++)
        {
            var c = normalised[i];

            switch (c)
            {
                case (>= 'a' and <= 'z') or (>= '0' and <= '9'):
                    {
                        if (prevDash)
                        {
                            sb.Append('-');
                            prevDash = false;
                        }

                        sb.Append(c);
                        break;
                    }
                case >= 'A' and <= 'Z':
                    {
                        if (prevDash)
                        {
                            sb.Append('-');
                            prevDash = false;
                        }

                        // Tricky way to convert to lowercase
                        if (toLower)
                        {
                            sb.Append((char)(c | 32));
                        }
                        else
                        {
                            sb.Append(c);
                        }

                        break;
                    }
                case ' ' or ',' or '.' or '/' or '\\' or '-' or '_' or '=':
                    {
                        if (!prevDash && sb.Length > 0)
                        {
                            prevDash = true;
                        }

                        break;
                    }
                default:
                    {
                        var swap = ConvertEdgeCases(c, toLower);

                        if (swap != null)
                        {
                            if (prevDash)
                            {
                                sb.Append('-');
                                prevDash = false;
                            }

                            sb.Append(swap);
                        }

                        break;
                    }
            }

            if (sb.Length == MaxLength)
            {
                break;
            }
        }

        return sb.ToString();
    }

    private static string? ConvertEdgeCases(char c, bool toLower)
    {
        return c switch
        {
            'ı' => "i",
            'ł' => "l",
            'Ł' => toLower ? "l" : "L",
            'đ' => "d",
            'ß' => "ss",
            'ø' => "o",
            'Þ' => "th",
            _ => null
        };
    }
}
