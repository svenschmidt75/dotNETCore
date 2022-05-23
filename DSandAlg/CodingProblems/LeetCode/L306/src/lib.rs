// 306. Additive Number
// https://leetcode.com/problems/additive-number/

struct Solution;

impl Solution {

    fn solve(v1: u64, v2: u64, cnt: u16, num: &str, idx: usize) -> bool {
        // SS: find number v3 s.t. v3 = v1 + v2
        let target = v1 + v2;
        let mut v3 = 0;
        let mut v3_pos = idx;

        // SS: terminating condition
        if idx == num.len() {
            return cnt > 2;
        }

        loop {
            if v3_pos == num.len() {
                return false;
            }

            loop {
                let c = num.chars().nth(v3_pos).unwrap() as u64 - '0' as u64;
                v3 = v3 * 10 + c;

                if v3_pos == num.len() - 1 {
                    // SS: no trailing 0s
                    break;
                }

                if num.chars().nth(v3_pos + 1).unwrap() != '0' {
                    break;
                }

                // SS: include following 0
                v3_pos += 1;
            }

            if v3 == target {
                if Solution::solve(v2, v3, cnt + 1, num, v3_pos + 1)
                {
                    return true;
                }
            } else if v3 > target {
               return false;
            }

            v3_pos += 1;
        }
    }

    pub fn is_additive_number(num: String) -> bool {
        if num.len() < 3 {
            return false;
        }

        let mut v1 = 0;
        let mut v2;

        let mut v1_pos = 0;
        let mut v2_pos ;

        loop {
            if v1_pos == num.len() {
                // done
                break;
            }

            loop {
                let c = num.chars().nth(v1_pos).unwrap() as u64 - '0' as u64;
                v1 = v1 * 10 + c;

                if v1_pos == num.len() - 1 {
                    // SS: no trailing 0s
                    break;
                }

                if num.chars().nth(v1_pos + 1).unwrap() != '0' {
                    break;
                }

                // SS: include following 0
                v1_pos += 1;
            }

            v2_pos = v1_pos + 1;
            v2 = 0;

            loop {
                if v2_pos == num.len() {
                    // SS: try advancing v1_pos
                    break;
                }

                loop {
                    let c = num.chars().nth(v2_pos).unwrap() as u64 - '0' as u64;
                    v2 = v2 * 10 + c;

                    if v2_pos == num.len() - 1 {
                        // SS: no trailing 0s
                        break;
                    }

                    if num.chars().nth(v2_pos + 1).unwrap() != '0' {
                        break;
                    }

                    // SS: include following 0
                    v2_pos += 1;
                }

                if Solution::solve(v1, v2, 2, &num, v2_pos + 1) {
                    return true;
                }

                v2_pos += 1;
            }

            v1_pos += 1;
        }

        false
    }
}


#[cfg(test)]
mod tests {
    use crate::Solution;

    #[test]
    fn test1() {
        // Arrange

        // Act
        let result = Solution::is_additive_number("112358".to_owned());

        // Assert
        assert!(result)
    }

    #[test]
    fn test2() {
        // Arrange

        // Act
        let result = Solution::is_additive_number("199100199".to_owned());

        // Assert
        assert!(result)
    }

    #[test]
    fn test3() {
        // Arrange

        // Act
        let result = Solution::is_additive_number("1010".to_owned());

        // Assert
        assert_eq!(false, result)
    }

    #[test]
    fn test4() {
        // Arrange

        // Act
        let result = Solution::is_additive_number("101120".to_owned());

        // Assert
        assert_eq!(false, result)
    }

    #[test]
    fn test5() {
        // Arrange

        // Act
        let result = Solution::is_additive_number("101".to_owned());

        // Assert
        assert_eq!(true, result)
    }

}
