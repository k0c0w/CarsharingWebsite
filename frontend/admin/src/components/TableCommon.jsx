import { Button, MenuItem } from '@mui/material'
import { useRef, useState } from 'react'
import { styleTextField } from '../styleComponents'
import { tokens } from '../theme'
import { useTheme } from '@emotion/react'


export function TableAddRefreshButtons ({ addHandler, refreshHandler }) {
  const theme = useTheme()
  const color = tokens(theme.palette.mode)
  return (
    <>
      <Button
        style={{
          backgroundColor: color.greenAccent[300],
          color: color.primary[900],
          marginRight: '20px'
        }}
        onClick={e => addHandler()}
      >
        Добавить
      </Button>
      <Button
        style={{
          backgroundColor: color.greenAccent[300],
          color: color.primary[900],
          marginRight: '20px'
        }}
        onClick={e => refreshHandler()}
      >
        Обновить
      </Button>
    </>
  )
}

const getAttr = (attrs, value) => {
  let result = null
  attrs.forEach(attr => {
    if (attr.value === value) result = attr
  })
  return result
}

function getFilteredList (oldList, value, attr) {
  const proccessed_value = value?.trimEnd()?.trimStart()
  debugger;
  if (proccessed_value.length < 1) return oldList
  const lowerAttr = attr.toLowerCase()
  const newList = oldList.filter(x => x[lowerAttr] == proccessed_value)
  return newList
}

export const TableSearchField = ({
  attrs,
  defaultAttrName,
  data,
  setData,
  customSearchFunc
}) => {
  const theme = useTheme()
  const color = tokens(theme.palette.mode)
  const StyledTextField = styleTextField(color.primary[100])
  const [attr, setAttr] = useState(defaultAttrName)
  const serachFieldRef = useRef(null)
  debugger
  const searchFunc = customSearchFunc ? customSearchFunc : getFilteredList

  console.log(attr)

  return (
    <div style={{ marginTop: '70px' }}>
      <StyledTextField
        ref={serachFieldRef}
        style={{ width: '250px' }}
        type={'search'}
        helperText={`Фильтровать по ${
          getAttr(attrs, attr)?.labelHelper?.toLowerCase() ?? attr
        }`}
      ></StyledTextField>
      <StyledTextField
        style={{ width: '170px' }}
        select
        label='Аттрибут'
        defaultValue={attr}
        helperText=''
        onChange={e => setAttr(e.target.value)}
      >
        {attrs.map(option => (
          <MenuItem key={option.value} value={option.value}>
            {option.label}
          </MenuItem>
        ))}
      </StyledTextField>
      <Button
        style={{
          marginTop: '10px',
          marginLeft: '20px',
          backgroundColor: color.primary[100],
          color: color.primary[900]
        }}
        variant={'contained'}
        onClick={() => {
          setData(
            searchFunc(
              data,
              serachFieldRef.current.childNodes[0].childNodes[0].value,
              attr
            )
          )
        }}
      >
        Добавить новый фильтр
      </Button>
    </div>
  )
}
