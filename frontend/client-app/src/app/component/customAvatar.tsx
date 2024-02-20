import * as React from 'react';
import Avatar from '@mui/material/Avatar';

interface CostomAvatarProps {
  fullName: string | null | undefined;
  size?: number | null | undefined;
  inactivated?: boolean;
}

export default function CostomAvatar(report: CostomAvatarProps) {
  if (report.inactivated) {
    return;
  }
  let shortName;
  function splitName(): string {
    if (!report.fullName) {
      return 'NA';
    }

    if (report.fullName.length > 1) {
      return report.fullName.slice(0, 2).toUpperCase();
    }
    return report.fullName.slice(0, 1).toUpperCase();
  }

  function stringToColor(string: string) {
    let hash = 0;
    let i;

    for (i = 0; i < string.length; i += 1) {
      hash = string.charCodeAt(i) + ((hash << 5) - hash);
    }

    let color = '#';
    for (i = 0; i < 3; i += 1) {
      const value = (hash >> (i * 8)) & 0xff;
      color += `00${value.toString(16)}`.slice(-2);
    }

    return color;
  }

  function colorFromName() {
    const bgColorStyle = {
      bgcolor: 'deepPurple[500]',
      width: report.size ?? 45,
      height: report.size ?? 45
    };

    if (report.fullName) {
      bgColorStyle.bgcolor = stringToColor(report.fullName);
    }
    return bgColorStyle;
  }

  return <Avatar sx={colorFromName()}>{splitName()}</Avatar>;
}
