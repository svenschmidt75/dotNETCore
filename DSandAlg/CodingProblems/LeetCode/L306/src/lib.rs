// 306. Additive Number
// https://leetcode.com/problems/additive-number/

struct Solution;

impl Solution {

    fn solve(num: &str, idx: usize, stack: &mut Vec<u64>) -> bool {
        let mut pos = idx;
        let mut v = 0;

        // SS: terminating condition
        if idx == num.len() {
            for i in 2..stack.len() {
                if stack[i - 2] + stack[i - 1] != stack[i] {
                    return false;
                }
            }

            return stack.len() > 2;
        }

        loop {
            if pos == num.len() {
                return false;
            }

            let c = num.chars().nth(pos).unwrap() as u64 - '0' as u64;
            v = v * 10 + c;

            stack.push(v);
            if Solution::solve(&num, pos + 1, stack) {
                return true;
            }
            stack.pop();

            if v == 0 {
                // SS: no leading 0s
                return false;
            }

            if pos == num.len() - 1 {
                // SS: no trailing 0s
                return false;
            }

            // SS: include following 0
            pos += 1;
        }
    }

    pub fn is_additive_number(num: String) -> bool {
        if num.len() < 3 {
            return false;
        }

        let mut stack = vec![];
        Solution::solve(&num, 0, &mut stack)
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

    #[test]
    fn test6() {
        // Arrange

        // Act
        let result = Solution::is_additive_number("101011".to_owned());

        // Assert
        assert_eq!(false, result)
    }

}
