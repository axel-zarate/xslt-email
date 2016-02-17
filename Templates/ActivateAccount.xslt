<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" indent="yes" />

	<xsl:template match="/*">

		<table width="600" border="0" cellspacing="0" cellpadding="0" style="font-family: arial,helvetica,verdana,sans-serif;
										background-color: #ffffff; border: solid 1px #dddddd; -moz-border-radius: 4px;
										-webkit-border-radius: 4px; border-radius: 4px; line-height: normal;">
			<tbody>
				<tr>
					<td height="20" colspan="3"></td>
				</tr>
				<tr>
					<td width="20"></td>
					<td>
						<img alt="Our Company"
							height="60" style="display: block; border: 0px;">
							<xsl:attribute name="src">
								<xsl:value-of select="Logo" disable-output-escaping="yes" />
							</xsl:attribute>
						</img>
					</td>
					<td width="20"></td>
				</tr>
				<tr>
					<td height="20" colspan="3">
					</td>
				</tr>
				<tr>
					<td width="20">
						
					</td>
					<td style="font-family: Arial, Helvetica, sans-serif; font-size: 14px; color: #666;">

						<p>
							Hi <xsl:value-of select="FirstName" /><xsl:text> </xsl:text><xsl:value-of select="LastName" />,
						</p>

						<p>
							Your account has been set up with user name <strong>
								<xsl:value-of select="Username" />
							</strong>.
						</p>

						<xsl:if test="IsPremiumUser = 'true'">
							<p>
								As a premium user you can enjoy all of the following benefits: <xsl:value-of select="IsPremiumUser" />
							</p>
							<ul>
								<xsl:for-each select="Benefits/BenefitsItem">
									<li><xsl:value-of select="current()" /></li>
								</xsl:for-each>
							</ul>
						</xsl:if>
						
						<p>To continue the process and activate your account, use the following link:</p>
						<p>
							<a target="_blank">
								<xsl:attribute name="href">
									<xsl:value-of select="ActivationLink" disable-output-escaping="yes" />
								</xsl:attribute>
								<xsl:value-of select="ActivationLink" disable-output-escaping="yes" />
							</a>
						</p>

						<p>
							Sincerely <br />
							Our Company
						</p>
					</td>
					<td width="20">
						<xsl:text> </xsl:text>
					</td>
				</tr>
				<tr>
					<td height="20" colspan="3">
					</td>
				</tr>
			</tbody>
		</table>
	</xsl:template>
</xsl:stylesheet>