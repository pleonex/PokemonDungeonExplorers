# Key Information

Position | Size | Description
-------- | ---- | -----------
0x00     | 0x01 | Password type: 0-4=SOS, 5,7=A-OK, 6=Thank-You
0x04     | 0x01 | Location ID
0x05     | 0x01 | Floor number
0x08     | 0x03 | Unknown
0x0C     | 0x04 | Unknown
0x10     | 0x04 | Unknown
0x14     | 0x08 | Message ID
0x1C     | 0x01 | Client name type: 08=Pokémones, 01=Custom
0x1D     | 0x0A | Custom client name
0xA0     | 0x02 | Unknown
0xA2     | 0x02 | Unknown
0xA4     | 0x08 | Unknown
0xAC     | 0x01 | Remaining mission attempts
0xAD     | 0x01 | Unknown
0xAE     | 0x01 | Game type: 00=Time, 01=Darkness, 02=Sky
