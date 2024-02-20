import * as React from 'react';
import { styled } from '@mui/material/styles';

import Tooltip, { TooltipProps, tooltipClasses } from '@mui/material/Tooltip';

import SvgIcon from '@mui/material/SvgIcon';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import Box from '@mui/material/Box';

interface DropDownListProps {
  list?: (string | null | undefined)[];
  id: string;
}

export default function DropDownList(report: DropDownListProps) {
  if (!report.list) {
    return;
  }

  let visibleText: string = '';
  let invisibleText: string[] = [];

  let colorSvgIcon = 'disabled';
  if (report.list.length > 1) {
    colorSvgIcon = 'primary';
  }

  for (let i = 0; i < report.list.length; i++) {
    if (i == 0) {
      visibleText = report.list[0]!;
    } else {
      invisibleText[i - 1] = report.list[i]!;
    }
  }

  const HtmlTooltip = styled(({ className, ...props }: TooltipProps) => (
    <Tooltip {...props} classes={{ popper: className }} />
  ))(({ theme }) => ({
    [`& .${tooltipClasses.tooltip}`]: {
      backgroundColor: '#f5f5f9',
      color: 'rgba(0, 0, 0, 0.87)',
      maxWidth: 220,
      fontSize: theme.typography.pxToRem(12),
      border: '1px solid #dadde9'
    }
  }));

  return (
    <HtmlTooltip
      title={
        <React.Fragment>
          {invisibleText.map((x) => (
            <p key={x}>{x}</p>
          ))}
        </React.Fragment>
      }>
      <Box>
        <Box>{visibleText}</Box>
        <SvgIcon component={ExpandMoreIcon} htmlColor={colorSvgIcon} />
      </Box>
    </HtmlTooltip>
  );
}
