using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Composition
{
    public enum WordType
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        DefaultUnknown,

        /// <summary>
        /// ADJ: adjective.
        /// </summary>
        Adjective,

        /// <summary>
        /// ADP: adposition.
        /// </summary>
        Adposition,

        /// <summary>
        /// ADV: adverb.
        /// </summary>
        Adverb,

        /// <summary>
        /// AUX: auxiliary.
        /// </summary>
        Auxiliary,

        /// <summary>
        /// CCONJ: coordinating conjunction.
        /// </summary>
        CoordinatingConjunction,

        /// <summary>
        /// DET: determiner.
        /// </summary>
        Determiner,

        /// <summary>
        /// INTJ: interjection.
        /// </summary>
        Interjection,

        /// <summary>
        /// NOUN: noun.
        /// </summary>
        Noun,

        /// <summary>
        /// NUM: numeral.
        /// </summary>
        Numeral,

        /// <summary>
        /// PART: particle.
        /// </summary>
        Particle,

        /// <summary>
        /// PRON: pronoun.
        /// </summary>
        Pronoun,

        /// <summary>
        /// PROPN: proper noun.
        /// </summary>
        ProperNoun,

        /// <summary>
        /// PUNCT: punctuation.
        /// </summary>
        Punctuation,

        /// <summary>
        /// SCONJ: subordinating conjunction.
        /// </summary>
        SubordinatingConjunction,

        /// <summary>
        /// SYM: symbol.
        /// </summary>
        Symbol,

        /// <summary>
        /// VERB: verb.
        /// </summary>
        Verb,

        /// <summary>
        /// X: other.
        /// </summary>
        Other,
    }
}
