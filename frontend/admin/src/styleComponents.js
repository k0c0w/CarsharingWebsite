import { TextField } from "@mui/material";
import styled from "@emotion/styled";


// export const styleTextField = (color) => styled(TextField)`
// & label.Mui-focused {
//     color: "${color}";
//   }
//   & .MuiInput-underline:after {
//     borderBottomColor: "${color}";
//   }
//   & .MuiOutlinedInput-root {
//     & fieldset {
//       borderColor: "${color}";
//     }
//     &:hover fieldset {
//       borderColor: "${color}";
//     }
//     &.Mui-focused fieldset {
//       borderColor: "${color}";
//     }
//   }
// `;


export const styleTextField = (color) => styled(TextField)`
& label.Mui-focused {
  color: ${color};
}
& .MuiOutlinedInput-root {
  &.Mui-focused fieldset {
    border-color:  ${color};
  }
}
`;

